using API_WebApplication.Models;

namespace API_WebApplication.Responses.DiemDanh
{
    public class GetDiemDanhResponse: BaseResponse
    {
        public List<DiemDanhModel>? DiemDanhs { get; set; }
    }
}
