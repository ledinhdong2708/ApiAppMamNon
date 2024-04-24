using API_WebApplication.Interfaces.TinTuc;
using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.TinTuc;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.TinTucs
{
    public class TinTucService : ITinTucService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public TinTucService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteTinTucResponse> DeleteTinTuc(int TinTucId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var tintuc = await _aPI_Application_V1Context.TinTucModels.Where(o => o.Id == TinTucId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var tintuc = await _aPI_Application_V1Context.TinTucModels.FindAsync(TinTucId);
            if (tintuc == null)
            {
                return new DeleteTinTucResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (tintuc.UserId != userId)
            {
                return new DeleteTinTucResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.TinTucModels.Remove(tintuc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteTinTucResponse
                {
                    Success = true,
                    TinTucId = tintuc.Id
                };
            }
            return new DeleteTinTucResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<TinTucResponse> GetIDTinTuc(int userId, int TinTucId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _tintuc = await _aPI_Application_V1Context.TinTucModels.Where(o => o.Id == TinTucId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _tintuc = await _aPI_Application_V1Context.TinTucModels.FindAsync(TinTucId);
            if (_tintuc == null)
            {
                return new TinTucResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_tintuc.UserId != userId)
            {
                return new TinTucResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_tintuc.Id < 0)
            {
                return new TinTucResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new TinTucResponse
            {
                Success = true,
                TinTuc = _tintuc
            };
        }

        public async Task<GetTinTucResponse> GetTinTucByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var tintuc = await _aPI_Application_V1Context.TinTucModels.Where(o => o.AppID == appID!.AppID).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetTinTucResponse { Success = true, TinTucs = tintuc.ToList() };
        }

        public async Task<SaveTinTucResponse> SaveTinTuc(TinTucModel TinTuc)
        {
            await _aPI_Application_V1Context.TinTucModels.AddAsync(TinTuc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveTinTucResponse
                {
                    Success = true,
                    TinTuc = TinTuc
                };
            }
            return new SaveTinTucResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateTinTucResponse> UpdateTinTuc(int TinTucId, TinTucModel TinTuc)
        {
            var tintucById = await _aPI_Application_V1Context.TinTucModels.FindAsync(TinTucId);
            if (tintucById == null)
            {
                return new UpdateTinTucResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (TinTuc.UserId != tintucById.UserId)
            {
                return new UpdateTinTucResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            tintucById.Title = TinTuc.Title;
            tintucById.Content = TinTuc.Content;
            tintucById.UpdateDate = TinTuc.UpdateDate;
            tintucById.IsCompleted = TinTuc.IsCompleted;
            tintucById.AppID = TinTuc.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateTinTucResponse
                {
                    Success = true,
                    TinTuc = TinTuc
                };
            }
            return new UpdateTinTucResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<GetTinTucResponse> PH_GetTinTucBy(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var tintuc = await _aPI_Application_V1Context.TinTucModels.Where(o => o.AppID == appID!.AppID).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetTinTucResponse { Success = true, TinTucs = tintuc.ToList() };
        }
    }
}
