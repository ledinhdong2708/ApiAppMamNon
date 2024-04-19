using API_WebApplication.Models;

namespace API_WebApplication.Responses.KhoaHocs
{
    public class GetKhoaHocResponse : BaseResponse
    {
        public List<KhoaHocModel> KhoaHocs { get; set; }
    }
}
