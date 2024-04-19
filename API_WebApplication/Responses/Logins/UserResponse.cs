using API_WebApplication.Models;

namespace API_WebApplication.Responses.Logins
{
    public class UserResponse: BaseResponse
    {
        public User? User { get; set; }
    }
}
