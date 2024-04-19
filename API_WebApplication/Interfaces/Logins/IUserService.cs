using API_WebApplication.Requests;
using API_WebApplication.Responses.Logins;
using API_WebApplication.Responses;
using System.Threading.Tasks;
using API_WebApplication.Models;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.Students;
using API_WebApplication.Requests.Students;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Interfaces.Logins
{
    public interface IUserService
    {
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        Task<LogoutResponse> LogoutAsync(int userId);
        Task<GetUsersResponse> GetUserRole1(string userId);
        Task<UserResponse> GetIDUser(int userId);
        Task<UpdateUserResponse> UpdateUser(int UserId, User user);
        Task<GetUsersResponse> GetUserGiaoVien(string userId);
        Task<GetUsersResponse> GetUserPhuHuynh(string userId);
        Task<UpdateUserResponse> UploadImageAvatar([FromForm] UploadFileStudentModel fileStudentModel, int idUser);
    }
}
