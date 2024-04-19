using API_WebApplication.Models;

namespace API_WebApplication.Responses.DinhDuong
{
    public class GetDinhDuongResponse : BaseResponse
    {
        public List<DinhDuongModel> DinhDuongs { get; set; }
    }
}
