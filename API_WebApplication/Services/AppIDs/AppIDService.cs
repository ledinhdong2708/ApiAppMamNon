using API_WebApplication.Interfaces.AppIDs;
using API_WebApplication.Models;
using API_WebApplication.Responses.AppIDs;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.TinTuc;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.AppIDs
{
    public class AppIDService : IAppIDService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public AppIDService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteAppIDResponse> DeleteAppID(int AppIDId, int userId)
        {
            var appID = await _aPI_Application_V1Context.AppIDs.FindAsync(AppIDId);
            if (appID == null)
            {
                return new DeleteAppIDResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            _aPI_Application_V1Context.AppIDs.Remove(appID);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteAppIDResponse
                {
                    Success = true,
                    appID_ID = appID.Id
                };
            }
            return new DeleteAppIDResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetAppIDResponse> GetAppIDByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.AppIDs.Where(o => o.UserId == userId.ToString()).ToListAsync();
            return new GetAppIDResponse { Success = true, appIDs = appID.ToList() };
        }

        public async Task<AppIDResponse> GetIDAppID(int userId, int AppIDId)
        {
            var _appID = await _aPI_Application_V1Context.AppIDs.FindAsync(AppIDId);
            if (_appID == null)
            {
                return new AppIDResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_appID.Id < 0)
            {
                return new AppIDResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new AppIDResponse
            {
                Success = true,
                appID = _appID
            };
        }

        public async Task<SaveAppIDResponse> SaveAppID(AppID AppID)
        {
            await _aPI_Application_V1Context.AppIDs.AddAsync(AppID);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveAppIDResponse
                {
                    Success = true,
                    appID = AppID
                };
            }
            return new SaveAppIDResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateAppIDResponse> UpdateAppID(int AppIDId, AppID AppID)
        {
            var appIDById = await _aPI_Application_V1Context.AppIDs.FindAsync(AppIDId);
            if (appIDById == null)
            {
                return new UpdateAppIDResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }

            appIDById.Name = AppID.Name;
            appIDById.IPServer = AppID.IPServer;
            appIDById.Logo = AppID.Logo;
            appIDById.Address = AppID.Address;
            appIDById.UserId = AppID.UserId;
            appIDById.Field1 = AppID.Field1;
            appIDById.Field2 = AppID.Field2;
            appIDById.Field3 = AppID.Field3;
            appIDById.Field4 = AppID.Field4;
            appIDById.Field5 = AppID.Field5;
            appIDById.Field6 = AppID.Field6;
            appIDById.Field7 = AppID.Field7;
            appIDById.Field8 = AppID.Field8;
            appIDById.Field9 = AppID.Field9;
            appIDById.Field10 = AppID.Field10;
            appIDById.CreateDate = AppID.CreateDate;
            appIDById.UpdateDate = AppID.UpdateDate;
            appIDById.Status = AppID.Status;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateAppIDResponse
                {
                    Success = true,
                    appID = AppID
                };
            }
            return new UpdateAppIDResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
