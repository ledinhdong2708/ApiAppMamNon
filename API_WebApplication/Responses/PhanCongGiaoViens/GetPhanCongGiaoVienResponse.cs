

using API_WebApplication.Models;

namespace API_WebApplication.Responses.PhanCongGiaoViens
{
    public class GetPhanCongGiaoVienResponse : BaseResponse
    {
        public List<PhanCongGiaoVienModel> PhanCongGiaoViens { get; set; }
    }
}
