using API_WebApplication.Controllers.Gia;
using API_WebApplication.Models;
using API_WebApplication.Requests.GiaTiens;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.GiaTiens;

namespace API_WebApplication.Interfaces.GiaTiens
{
    public interface IGiaTienService
    {
        Task<UpdateGiaTiensResponse> UpdateGiaTiensResponse(int idGia , GiaTiensModel giaTiens);
        Task<NewGiaTiensResponse> NewGiatiensResponse(int IdGia, GiaTiensModel giaTiensModel);
        Task<getallItemResponse> getallItemResponse();
    }
}
