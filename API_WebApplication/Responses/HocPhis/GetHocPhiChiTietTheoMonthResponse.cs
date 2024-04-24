using API_WebApplication.Models;

namespace API_WebApplication.Responses.HocPhis
{
    public class GetHocPhiChiTietTheoMonthResponse : BaseResponse
    {
        public List<ChiTietHocPhiTheoMonth> ChiTietHocPhiTheoMonths { get; set; }
    }
}
