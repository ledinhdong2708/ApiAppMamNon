using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.Models;
using API_WebApplication.Responses.ThoiKhoaBieu;

namespace API_WebApplication.Interfaces.ThoiKhoaBieu
{
    public interface IThoiKhoaBieuServiceExample
    {
        Task<GetThoiKhoaBieusResponseExample> GetThoiKhoaBieusByUser(int userId);
        Task<SaveThoiKhoaBieuResponseExample> SaveThoiKhoaBieu(OTKBDTO ThoiKhoaBieu);
        Task<DeleteThoiKhoaBieuResponseExample> DeleteThoiKhoaBieu(int ThoiKhoaBieuId, int userId);
        Task<UpdateThoiKhoaBieuResponseExample> UpdateThoiKhoaBieu(int ThoiKhoaBieuId, OTKBDTO ThoiKhoaBieu);
        Task<ThoiKhoaBieuResponseExample> GetIDThoiKhoaBieu(int userId, int ThoiKhoaBieuId);
    }
}
