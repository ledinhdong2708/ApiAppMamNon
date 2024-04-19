using API_WebApplication.Models;

namespace API_WebApplication.Responses.SoBeNgoans
{
    public class GetSoBeNgoanResponse : BaseResponse
    {
        public List<SoBeNgoan> SoBeNgoans { get; set; }
    }
}
