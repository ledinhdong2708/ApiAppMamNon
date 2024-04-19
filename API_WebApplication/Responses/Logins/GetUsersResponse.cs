using API_WebApplication.Models;

namespace API_WebApplication.Responses.Logins
{
    public class GetUsersResponse : BaseResponse
    {
        public List<User> Users { get; set; }
    }
}
