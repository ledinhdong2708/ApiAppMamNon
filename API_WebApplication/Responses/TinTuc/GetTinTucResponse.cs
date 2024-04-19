using API_WebApplication.Models;

namespace API_WebApplication.Responses.TinTuc
{
    public class GetTinTucResponse :BaseResponse
    {
        public List<TinTucModel> TinTucs { get; set; }
    }
}
