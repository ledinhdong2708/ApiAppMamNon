using API_WebApplication.Models;

namespace API_WebApplication.Responses.AppIDs
{
    public class GetAppIDResponse : BaseResponse
    {
        public List<AppID> appIDs { get; set; }
    }
}
