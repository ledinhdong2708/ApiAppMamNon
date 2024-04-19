using API_WebApplication.Models;

namespace API_WebApplication.Responses.DanThuocs
{
    public class GetDanThuocResponse : BaseResponse
    {
        public List<DanThuocModel> DanThuocModels { get; set; }
    }
}
