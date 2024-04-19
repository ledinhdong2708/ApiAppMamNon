using API_WebApplication.Models;
using API_WebApplication.Responses.TinTuc;

namespace API_WebApplication.Interfaces.TinTuc
{
    public interface ITinTucService
    {
        Task<GetTinTucResponse> GetTinTucByUser(int userId);
        Task<SaveTinTucResponse> SaveTinTuc(TinTucModel TinTuc);
        Task<DeleteTinTucResponse> DeleteTinTuc(int TinTucId, int userId);
        Task<UpdateTinTucResponse> UpdateTinTuc(int TinTucId, TinTucModel TinTuc);
        Task<TinTucResponse> GetIDTinTuc(int userId, int TinTucId);
        Task<GetTinTucResponse> PH_GetTinTucBy(int userId);
    }
}
