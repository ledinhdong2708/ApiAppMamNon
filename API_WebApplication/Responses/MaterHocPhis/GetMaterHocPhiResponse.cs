using API_WebApplication.Models;

namespace API_WebApplication.Responses.MaterHocPhis
{
    public class GetMaterHocPhiResponse : BaseResponse
    {
        public List<MaterHocPhi> MaterHocPhis { get; set; }
    }
}
