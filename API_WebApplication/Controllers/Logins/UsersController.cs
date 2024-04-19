using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.Logins;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Requests.DiemDanh;
using API_WebApplication.Requests.Logins;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.Logins;
using API_WebApplication.Services.Logins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.Logins
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService, ITokenService tokenService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
        }
        [HttpPost]
        [Route("login")]
        //[Consumes("application/json")]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(TokenResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(TokenResponse), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing login details",
                    ErrorCode = "L01"
                });
            }
            var loginResponse = await _userService.LoginAsync(loginRequest);
            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.Error
                });
            }
            return Ok(loginResponse);
        }
        [HttpPost]
        [Route("refresh_token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken) || refreshTokenRequest.UserId == 0)
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing refresh token details",
                    ErrorCode = "R01"
                });
            }
            var validateRefreshTokenResponse = await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);
            if (!validateRefreshTokenResponse.Success)
            {
                return UnprocessableEntity(validateRefreshTokenResponse);
            }
            var tokenResponse = await _tokenService.GenerateTokensAsync(validateRefreshTokenResponse.UserId);
            return Ok(new { AccessToken = tokenResponse.Item1, Refreshtoken = tokenResponse.Item2 });
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new TokenResponse
                    {
                        Error = $"{string.Join(",", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            var signupResponse = await _userService.SignupAsync(signupRequest);
            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }
            return Ok(signupResponse.Username);
        }
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var logout = await _userService.LogoutAsync(UserID_Protected);
            if (!logout.Success)
            {
                return UnprocessableEntity(logout);
            }
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getStudentsResponse = await _userService.GetUserRole1(UserID_Protected.ToString());
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Users.ConvertAll(o => new User { 
                Id = o.Id, 
                Email = o.Email, 
                City = o.City,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.Phone,
                Address = o.Address,
                Address2 = o.Address2,
                CreateDate = o.CreateDate,
                Position = o.Position,
                Username = o.Username,
                Avatar = o.Avatar,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID)
        {
            var getUserResponse = await _userService.GetIDUser(UserID);
            if (!getUserResponse.Success)
            {
                return UnprocessableEntity(getUserResponse);
            }

            var tasksResponse = new User
            {
                Id = getUserResponse.User.Id,
                Username = getUserResponse.User.Username,
                Email = getUserResponse.User.Email,
                FirstName = getUserResponse.User.FirstName,
                LastName = getUserResponse.User.LastName,
                Phone = getUserResponse.User.Phone,
                City = getUserResponse.User.City,
                Address = getUserResponse.User.Address,
                Address2 = getUserResponse.User.Address2,
                CreateDate = getUserResponse.User.CreateDate,
                TS = getUserResponse.User.TS,
                Avatar = getUserResponse.User.Avatar,
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserRequest userUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var _user = new User
            {
                Email = userUpdateRequest.Email,
                FirstName = userUpdateRequest.FirstName,
                LastName = userUpdateRequest.LastName,
                Phone = userUpdateRequest.Phone,
                City = userUpdateRequest.City,
                Address = userUpdateRequest.Address,
                Address2 = userUpdateRequest.Address2,
                UpdateDate = dateTime
            };
            var saveUserResponse = await _userService.UpdateUser(id, _user);
            if (!saveUserResponse.Success)
            {
                return UnprocessableEntity(saveUserResponse);
            }
            var taskResponse = new User
            {
                Id = saveUserResponse.User.Id,
                Username = saveUserResponse.User.Username,
                Email = saveUserResponse.User.Email,
                FirstName = saveUserResponse.User.FirstName,
                LastName = saveUserResponse.User.LastName,
                Phone = saveUserResponse.User.Phone,
                City = saveUserResponse.User.City,
                Address = saveUserResponse.User.Address,
                Address2 = saveUserResponse.User.Address2,
                CreateDate = saveUserResponse.User.CreateDate,
                TS = saveUserResponse.User.TS,
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("giaovien")]
        public async Task<IActionResult> GetAllGiaoVien()
        {
            var getStudentsResponse = await _userService.GetUserGiaoVien(UserID_Protected.ToString());
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Users.ConvertAll(o => new User
            {
                Id = o.Id,
                Email = o.Email,
                City = o.City,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.Phone,
                Address = o.Address,
                Address2 = o.Address2,
                CreateDate = o.CreateDate,
                Position = o.Position,
                Username = o.Username,
                AppID = o.AppID,
                Avatar = o.Avatar,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("phuhuynh")]
        public async Task<IActionResult> GetAllPhuHuynh()
        {
            var getStudentsResponse = await _userService.GetUserPhuHuynh(UserID_Protected.ToString());
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Users.ConvertAll(o => new User
            {
                Id = o.Id,
                Email = o.Email,
                City = o.City,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.Phone,
                Address = o.Address,
                Address2 = o.Address2,
                CreateDate = o.CreateDate,
                Position = o.Position,
                Username = o.Username,
                Avatar = o.Avatar,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost("UploadFile")]
        //[RequestSizeLimit(1000 * 1024 * 1024 )]
        public async Task<IActionResult> UploadImageAvatar([FromForm] UploadFileStudentModel fileStudentModel)
        {
            var uniqueFileName = Guid.NewGuid().ToString();
            var stringCutted = fileStudentModel.file.FileName.Split('.').Last();
            var fileStudent = new UploadFileStudentModel
            {
                //FileName = fileStudentModel.file.FileName,
                FileName = uniqueFileName + "." + stringCutted,
                file = fileStudentModel.file
            };
            var saveStudentResponse = await _userService.UploadImageAvatar(fileStudent, UserID_Protected);
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }

            return Ok(saveStudentResponse);

        }
    }
}
