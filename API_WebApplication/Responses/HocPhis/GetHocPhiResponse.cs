using API_WebApplication.Models;

namespace API_WebApplication.Responses.HocPhis
{
    public class GetHocPhiResponse : BaseResponse
    {
        public List<HocPhi> HocPhis { get; set; }
    }
}
