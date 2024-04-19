using API_WebApplication.Interfaces.KhoaHocs;
using API_WebApplication.Models;
using API_WebApplication.Responses.KhoaHocs;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.KhoaHocs
{
    public class KhoaHocService : IKhoaHocService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public KhoaHocService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteKhoaHocResponse> DeleteKhoaHoc(int KhoaHocId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var KhoaHoc = await _aPI_Application_V1Context.KhoaHocModels.Where(o => o.Id == KhoaHocId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var KhoaHoc = await _aPI_Application_V1Context.KhoaHocModels.FindAsync(KhoaHocId);
            if (KhoaHoc == null)
            {
                return new DeleteKhoaHocResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (KhoaHoc.UserId != userId)
            {
                return new DeleteKhoaHocResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.KhoaHocModels.Remove(KhoaHoc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteKhoaHocResponse
                {
                    Success = true,
                    KhoaHocId = KhoaHoc.Id
                };
            }
            return new DeleteKhoaHocResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<KhoaHocResponse> GetIDKhoaHoc(int userId, int KhoaHocId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _KhoaHoc = await _aPI_Application_V1Context.KhoaHocModels.Where(o => o.Id == KhoaHocId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _KhoaHoc = await _aPI_Application_V1Context.KhoaHocModels.FindAsync(KhoaHocId);
            if (_KhoaHoc == null)
            {
                return new KhoaHocResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_KhoaHoc.UserId != userId)
            {
                return new KhoaHocResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_KhoaHoc.Id < 0)
            {
                return new KhoaHocResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new KhoaHocResponse
            {
                Success = true,
                KhoaHoc = _KhoaHoc
            };
        }

        public async Task<GetKhoaHocResponse> GetKhoaHocByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var KhoaHoc = await _aPI_Application_V1Context.KhoaHocModels.Where(o => o.AppID == appID!.AppID).ToListAsync();
            return new GetKhoaHocResponse { Success = true, KhoaHocs = KhoaHoc.ToList() };
        }

        public async Task<SaveKhoaHocResponse> SaveKhoaHoc(KhoaHocModel KhoaHoc)
        {
            await _aPI_Application_V1Context.KhoaHocModels.AddAsync(KhoaHoc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveKhoaHocResponse
                {
                    Success = true,
                    KhoaHoc = KhoaHoc
                };
            }
            return new SaveKhoaHocResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateKhoaHocResponse> UpdateKhoaHoc(int KhoaHocId, KhoaHocModel KhoaHoc)
        {
            var KhoaHocById = await _aPI_Application_V1Context.KhoaHocModels.FindAsync(KhoaHocId);
            if (KhoaHocById == null)
            {
                return new UpdateKhoaHocResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (KhoaHoc.UserId != KhoaHocById.UserId)
            {
                return new UpdateKhoaHocResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            KhoaHocById.FromYear = KhoaHoc.FromYear;
            KhoaHocById.ToYear = KhoaHoc.ToYear;
            KhoaHocById.UpdateDate = KhoaHoc.UpdateDate;
            KhoaHocById.IsCompleted = KhoaHoc.IsCompleted;
            KhoaHocById.AppID = KhoaHoc.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateKhoaHocResponse
                {
                    Success = true,
                    KhoaHoc = KhoaHoc
                };
            }
            return new UpdateKhoaHocResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
