using API_WebApplication.Models;

namespace API_WebApplication.Responses.Students
{
    public class GetStudentsResponse : BaseResponse
    {
        public List<Student> Students { get; set; }
    }
}
