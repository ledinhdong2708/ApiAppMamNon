using API_WebApplication.Helpers;
using API_WebApplication.Interfaces.Logins;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Responses.Logins;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.Logins
{
    public class TokenService : ITokenService
    {
        private readonly API_Application_V1Context _aPI_Application;
        public TokenService(API_Application_V1Context aPI_Application)
        {
            this._aPI_Application = aPI_Application;
        }
        public async Task<Tuple<string, string, string, string, string, string,string>> GenerateTokensAsync(int userId)
        {
            var userRecord = await _aPI_Application.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Id == userId);
            var accessToken = await TokenHelper.GenerateAccessToken(userId, userRecord.Role.ToString());
            var refreshToken = await TokenHelper.GenerateRefreshToken();
            if (userRecord == null)
            {
                return null;
            }
            var salt = PasswordHelper.GetSecureSalt();
            var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, salt);
            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);
            }
            userRecord.RefreshTokens?.Add(new RefreshToken
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                TS = DateTime.Now,
                UserId = userId,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt)
            });
            await _aPI_Application.SaveChangesAsync();

            var expiry = userRecord.RefreshTokens.ToList();

            var token = new Tuple<string, string, string, string, string,string, string>(
                accessToken, 
                refreshToken, 
                Base64Encode(userRecord.Username), 
                Base64Encode(userRecord.Role.ToString()), 
                Base64Encode(expiry[0].ExpiryDate.ToString()),
                userId.ToString(), 
                userRecord.StudentId.ToString()
                );
            return token;
        }
        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userRecord = await _aPI_Application.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.Id == user.Id);
            if (userRecord == null)
            {
                return false;
            }
            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                var currentRefreshToken = userRecord.RefreshTokens.First();
                _aPI_Application.RefreshTokens.Remove(currentRefreshToken);
            }
            return false;
        }
        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _aPI_Application.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == refreshTokenRequest.UserId);
            var response = new ValidateRefreshTokenResponse();
            if (refreshToken == null)
            {
                response.Success = false;
                response.Error = "Invalid session or user is already logged out";
                response.ErrorCode = "R02";
                return response;
            }
            var refreshTokenToValidateHash = PasswordHelper.HashUsingPbkdf2(refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));
            if (refreshToken.TokenHash != refreshTokenToValidateHash)
            {
                response.Success = false;
                response.Error = "Invalid refresh token";
                response.ErrorCode = "R03";
                return response;
            }

            if (refreshToken.ExpiryDate < DateTime.Now)
            {
                response.Success = false;
                response.Error = "Refresh token has expired";
                response.ErrorCode = "R04";
                return response;
            }
            response.Success = true;
            response.UserId = refreshToken.UserId;
            return response;
        }

        private string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
    }
}
