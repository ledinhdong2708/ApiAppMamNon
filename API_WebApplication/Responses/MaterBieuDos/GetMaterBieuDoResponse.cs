using API_WebApplication.Models;

namespace API_WebApplication.Responses.MaterBieuDos
{
    public class GetMaterBieuDoResponse : BaseResponse
    {
        public List<MaterBieuDo> MaterBieuDos { get; set; }
    }
}
