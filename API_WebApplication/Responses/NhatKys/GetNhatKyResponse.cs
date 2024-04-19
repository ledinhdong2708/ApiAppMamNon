using API_WebApplication.Models;

namespace API_WebApplication.Responses.NhatKys
{
    public class GetNhatKyResponse : BaseResponse
    {
        public List<NhatKy> NhatKys { get; set; }
    }
}
