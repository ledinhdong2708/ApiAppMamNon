using API_WebApplication.Models;
using API_WebApplication.Requests.NhatKy;

namespace API_WebApplication.Responses.NhatKys
{
    public class GetNhatKyGetAllResponse : BaseResponse
    {
        public List<NhatKyGetAll> NhatKys { get; set; }
    }
}
