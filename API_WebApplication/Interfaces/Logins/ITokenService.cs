using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Responses.Logins;

namespace API_WebApplication.Interfaces.Logins
{
    public interface ITokenService
    {
        Task<Tuple<string, string, string, string, string, string,string>> GenerateTokensAsync(int userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<bool> RemoveRefreshTokenAsync(User user);
    }
}
