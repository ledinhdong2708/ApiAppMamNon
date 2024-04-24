using API_WebApplication.Models;

namespace API_WebApplication.Responses.GiaTiens
{
    public class getallItemResponse :BaseResponse
    {
        public List<GiaTiensModel> giaTiensModels { get; set; }
    }
}
