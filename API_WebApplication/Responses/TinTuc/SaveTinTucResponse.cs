using API_WebApplication.Models;

namespace API_WebApplication.Responses.TinTuc
{
    public class SaveTinTucResponse : BaseResponse
    {
        public TinTucModel TinTuc { get; set; }
    }
}
