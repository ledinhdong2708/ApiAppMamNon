using API_WebApplication.Models;

namespace API_WebApplication.Responses.HoatDong
{
    public class GetHoatDongResponse:BaseResponse
    {
        public List<HoatDongModel> HoatDongs { get; set; }
    }
}
