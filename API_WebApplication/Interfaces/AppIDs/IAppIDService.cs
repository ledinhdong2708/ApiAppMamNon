using API_WebApplication.Models;
using API_WebApplication.Responses.AppIDs;

namespace API_WebApplication.Interfaces.AppIDs
{
    public interface IAppIDService
    {
        Task<GetAppIDResponse> GetAppIDByUser(int userId);
        Task<SaveAppIDResponse> SaveAppID(AppID AppID);
        Task<DeleteAppIDResponse> DeleteAppID(int AppIDId, int userId);
        Task<UpdateAppIDResponse> UpdateAppID(int AppIDId, AppID AppID);
        Task<AppIDResponse> GetIDAppID(int userId, int AppIDId);
    }
}
