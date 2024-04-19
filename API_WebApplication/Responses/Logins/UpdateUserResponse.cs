using API_WebApplication.Models;

namespace API_WebApplication.Responses.Logins
{
    public class UpdateUserResponse: BaseResponse
    {
        public User? User { get; set; }
    }
}
