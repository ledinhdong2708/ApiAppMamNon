using API_WebApplication.Models;

namespace API_WebApplication.Responses.Classs
{
    public class GetClassResponse : BaseResponse
    {
        public List<ClassModel> Classss { get; set; }
    }
}
