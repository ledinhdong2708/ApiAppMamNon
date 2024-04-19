using API_WebApplication.Models;

namespace API_WebApplication.Responses.ThoiKhoaBieuReponse
{
    public class GetThoiKhoaBieuResponse : BaseResponse
    {
        public List<ThoiKhoaBieuModel> ThoiKhoaBieus { get; set; }
    }
}
