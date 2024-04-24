using API_WebApplication.Helpers;
using API_WebApplication.Interfaces.Logins;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Responses.Logins;
using API_WebApplication.Responses;
using Microsoft.EntityFrameworkCore;
using API_WebApplication.Responses.Bases;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Requests.Students;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Services.Logins
{
    public class UserService : IUserService
    {
        private readonly API_Application_V1Context _aPI_Application;
        private readonly ITokenService _tokenService;
        public UserService(API_Application_V1Context aPI_Application, ITokenService tokenService)
        {
            this._aPI_Application = aPI_Application;
            this._tokenService = tokenService;
        }
        public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = _aPI_Application.Users.SingleOrDefault(user => user.Active && user.Username == loginRequest.Username);
            if (user == null)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Username not found",
                    ErrorCode = "L02"
                };
            }
            var passwordHash = PasswordHelper.HashUsingPbkdf2(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));
            if (user.Password != passwordHash)
            {
                return new TokenResponse
                {
                    Success = false,
                    Error = "Invalid Password",
                    ErrorCode = "L03"
                };
            }
            var token = await System.Threading.Tasks.Task.Run(() => _tokenService.GenerateTokensAsync(user.Id));
            return new TokenResponse
            {
                Success = true,
                AccessToken = token.Item1,
                RefreshToken = token.Item2,
                Username = token.Item3,
                Role = token.Item4,
                ExpiryDate = token.Item5,
                statusCode = StatusCodes.Status200OK.ToString(),
                userID = token.Item6,
                studentID = token.Item7,
                appID  = user.AppID.ToString(),
            };
        }
        public async Task<LogoutResponse> LogoutAsync(int userId)
        {
            var refreshToken = await _aPI_Application.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == userId);
            if (refreshToken == null)
            {
                return new LogoutResponse { Success = true };
            }
            _aPI_Application.RefreshTokens.Remove(refreshToken);
            var saveResponse = await _aPI_Application.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }
            return new LogoutResponse { Success = false, Error = "Unable to logout user", ErrorCode = "L04" };
        }
        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            DateTime dateTime = DateTime.Now;
            var existingUser = await _aPI_Application.Users.SingleOrDefaultAsync(user => user.Email == signupRequest.Email);
            if (existingUser != null)
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "User already exists with the same email",
                    ErrorCode = "S02"
                };
            }
            var exstingUsername = await _aPI_Application.Users.SingleOrDefaultAsync(user => user.Username == signupRequest.Username);
            if (exstingUsername != null)
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "Username already exists with the same Username",
                    ErrorCode = "S03"
                };
            }
            if (signupRequest.Password != signupRequest.ConfirmPassword)
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "Password and confirm password do not match",
                    ErrorCode = "S04"
                };
            }
            if (signupRequest.Password.Length <= 7) // This can be more complicated than only length, you can check on alphanumeric and or special characters
            {
                return new SignupResponse
                {
                    Success = false,
                    Error = "Password is weak",
                    ErrorCode = "S05"
                };
            }
            var salt = PasswordHelper.GetSecureSalt();
            var passwordHash = PasswordHelper.HashUsingPbkdf2(signupRequest.Password, salt);
            SignupRequest signupRequest1 = signupRequest;
            var IDStudent = 0;
            if (signupRequest.Role != 2)
            {
                IDStudent = _aPI_Application.Students.Max(p => p.Id);
            }
            //var IDStudent = _aPI_Application.Students.Max(p => p.Id);
            var user = new User
            {
                Email = signupRequest.Role != 3 ?  signupRequest.Email : (IDStudent +"_"+ signupRequest.Email),
                Password = passwordHash,
                PasswordSalt = Convert.ToBase64String(salt),
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
                Phone = signupRequest1?.Phone,
                City = signupRequest?.City,
                Address = signupRequest?.Address,
                Address2 = signupRequest?.Address2,
                Role = signupRequest.Role,
                //TS = signupRequest.Ts,
                //CreateDate = signupRequest.CreateDate,
                TS = dateTime,
                CreateDate = dateTime,
                UpdateDate = dateTime,
                Username = signupRequest.Role != 3 ? signupRequest.Username : (signupRequest.Username + IDStudent),
                Active = true,
                StudentId = IDStudent,
                AppID = signupRequest.AppID,
            };
            await _aPI_Application.Users.AddAsync(user);
            var saveResponse = await _aPI_Application.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new SignupResponse { Success = true, Username = user.Username, Email = user.Email };
            }
            return new SignupResponse
            {
                Success = false,
                Error = "Unable to save the user",
                ErrorCode = "S06"
            };
        }
        public async Task<GetUsersResponse> GetUserRole1(string userId)
        {
            var appID = await _aPI_Application.Users.Where(o => o.Id.ToString() == userId).FirstOrDefaultAsync();
            var user = await _aPI_Application.Users.Where(o => o.Role == 1 && o.AppID == appID!.AppID).ToListAsync();
            return new GetUsersResponse { Success = true, Users = user.ToList() };
        }
        public async Task<UserResponse> GetIDUser(int userId)
        {
            var _user = await _aPI_Application.Users.FindAsync(userId);
            if (_user == null)
            {
                return new UserResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_user.Id < 0)
            {
                return new UserResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new UserResponse
            {
                Success = true,
                User = _user
            };
        }

        public async Task<UpdateUserResponse> UpdateUser(int UserId, User _user)
        {
            var userById = await _aPI_Application.Users.FindAsync(UserId);
            if (userById == null)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            userById.Email = _user.Email;
            userById.FirstName = _user.FirstName;
            userById.LastName = _user.LastName;
            userById.Phone = _user.Phone;
            userById.City = _user.City;
            userById.Address = _user.Address;
            userById.Address2 = _user.Address2;
            userById.UpdateDate = _user.UpdateDate;

            var updateResponse = await _aPI_Application.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateUserResponse
                {
                    Success = true,
                    User = _user
                };
            }
            return new UpdateUserResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetUsersResponse> GetUserGiaoVien(string userId)
        {
            var appID = await _aPI_Application.Users.Where(o => o.Id.ToString() == userId).FirstOrDefaultAsync();
            var user = await _aPI_Application.Users.Where(o => o.Role == 2 && o.AppID == appID!.AppID).ToListAsync();
            return new GetUsersResponse { Success = true, Users = user.ToList() };
        }
        public async Task<GetUsersResponse> GetUserPhuHuynh(string userId)
        {
            var appID = await _aPI_Application.Users.Where(o => o.Id.ToString() == userId).FirstOrDefaultAsync();
            var user = await _aPI_Application.Users.Where(o => o.Role == 3 && o.AppID == appID!.AppID).ToListAsync();
            return new GetUsersResponse { Success = true, Users = user.ToList() };
        }

        public async Task<UpdateUserResponse> UploadImageAvatar([FromForm] UploadFileStudentModel fileStudentModel, int idUser)
        {
            var _user = await _aPI_Application.Users.FindAsync(idUser);
            if (_user == null)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    Error = "User not found",
                    ErrorCode = "T01"
                };
            }

            //string root = @"C:\imageAPIApp\" + idUser;
            string root = @"upload\avartar" + idUser;
            // If directory does not exist, create it.
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string patch = Path.Combine(root, fileStudentModel.FileName);
            //string patch = Path.Combine(root, fileStudentModel.FileName);
            using (Stream stream = new FileStream(patch, FileMode.Create))
            {
                await fileStudentModel.file.CopyToAsync(stream);
                stream.Close();

            }
            _user.Avatar = patch;
            await _aPI_Application.SaveChangesAsync();
            return new UpdateUserResponse
            {
                Success = true,
                User = _user,
            };
        }
    }
}
