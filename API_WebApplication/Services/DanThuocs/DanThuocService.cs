using API_WebApplication.Interfaces.DanThuocs;
using API_WebApplication.Models;
using API_WebApplication.Responses.DanThuocs;
using API_WebApplication.Responses.MaterBieuDos;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.DanThuocs
{
    public class DanThuocService : IDanThuocService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public DanThuocService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteDanThuocResponse> DeleteDanThuoc(int DanThuocId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var danThuoc = await _aPI_Application_V1Context.DanThuocModels.Where(o => o.Id == DanThuocId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var danThuoc = await _aPI_Application_V1Context.DanThuocModels.FindAsync(DanThuocId);
            if (danThuoc == null)
            {
                return new DeleteDanThuocResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (danThuoc.UserId != userId)
            {
                return new DeleteDanThuocResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.DanThuocModels.Remove(danThuoc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteDanThuocResponse
                {
                    Success = true,
                    DanThuocId = danThuoc.Id
                };
            }
            return new DeleteDanThuocResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetDanThuocResponse> GetDanThuocByStudent(int userId, int studentId, string day, string month, string year)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var danThuoc = await _aPI_Application_V1Context.DanThuocModels.Where(o =>
            //o.UserId == userId && 
            o.AppID == appID!.AppID &&
            //o.StudentId == studentId &&
            o.DocDate.Value.Day.ToString() == day &&
            o.DocDate.Value.Month.ToString()== month &&
            o.DocDate.Value.Year.ToString() == year
            ).OrderByDescending(o => o.DocDate).ToListAsync();
            return new GetDanThuocResponse { Success = true, DanThuocModels = danThuoc.ToList() };
        }

        public async Task<GetDanThuocResponse> GetDanThuocByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var danThuoc = await _aPI_Application_V1Context.DanThuocModels.Where(o => o.UserId == userId && o.AppID == appID!.AppID).ToListAsync();
            return new GetDanThuocResponse { Success = true, DanThuocModels = danThuoc.ToList() };
        }

        public async Task<DanThuocResponse> GetIDDanThuoc(int userId, int DanThuocId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _danThuoc = await _aPI_Application_V1Context.DanThuocModels.Where(o => o.Id == DanThuocId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _danThuoc = await _aPI_Application_V1Context.DanThuocModels.FindAsync(DanThuocId);
            if (_danThuoc == null)
            {
                return new DanThuocResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_danThuoc.UserId != userId)
            {
                return new DanThuocResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_danThuoc.Id < 0)
            {
                return new DanThuocResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DanThuocResponse
            {
                Success = true,
                DanThuoc = _danThuoc
            };
        }

        public async Task<SaveDanThuocResponse> SaveDanThuoc(DanThuocModel DanThuoc)
        {
            await _aPI_Application_V1Context.DanThuocModels.AddAsync(DanThuoc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveDanThuocResponse
                {
                    Success = true,
                    DanThuoc = DanThuoc
                };
            }
            return new SaveDanThuocResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<SaveDanThuocResponse> PH_SaveDanThuoc(DanThuocModel DanThuoc)
        {
            await _aPI_Application_V1Context.DanThuocModels.AddAsync(DanThuoc);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveDanThuocResponse
                {
                    Success = true,
                    DanThuoc = DanThuoc
                };
            }
            return new SaveDanThuocResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateDanThuocResponse> UpdateDanThuoc(int DanThuocId, DanThuocModel DanThuoc)
        {
            var danThuocById = await _aPI_Application_V1Context.DanThuocModels.FindAsync(DanThuocId);
            if (danThuocById == null)
            {
                return new UpdateDanThuocResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (DanThuoc.UserId != danThuocById.UserId)
            {
                return new UpdateDanThuocResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            danThuocById.DocDate = DanThuoc.DocDate;
            danThuocById.Content = DanThuoc.Content;
            danThuocById.StudentId = DanThuoc.StudentId;
            danThuocById.UpdateDate = DanThuoc.UpdateDate;
            danThuocById.IsCompleted = DanThuoc.IsCompleted;
            danThuocById.AppID = DanThuoc.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateDanThuocResponse
                {
                    Success = true,
                    DanThuocModel = DanThuoc
                };
            }
            return new UpdateDanThuocResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateDanThuocResponse> PH_UpdateDanThuoc(int DanThuocId, DanThuocModel DanThuoc)
        {
            var danThuocById = await _aPI_Application_V1Context.DanThuocModels.FindAsync(DanThuocId);
            if (danThuocById == null)
            {
                return new UpdateDanThuocResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            //if (DanThuoc.UserId != danThuocById.UserId)
            //{
            //    return new UpdateDanThuocResponse
            //    {
            //        Success = false,
            //        Error = "You don't have access to get id this ThoiKhoaBieu",
            //        ErrorCode = "T02"
            //    };
            //}

            danThuocById.DocDate = DanThuoc.DocDate;
            danThuocById.Content = DanThuoc.Content;
            danThuocById.StudentId = DanThuoc.StudentId;
            danThuocById.UpdateDate = DanThuoc.UpdateDate;
            danThuocById.IsCompleted = DanThuoc.IsCompleted;
            danThuocById.AppID = DanThuoc.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateDanThuocResponse
                {
                    Success = true,
                    DanThuocModel = DanThuoc
                };
            }
            return new UpdateDanThuocResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetDanThuocResponse> PH_GetDanThuocByStudent(int userId, int studentId, string day, string month, string year)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var danThuoc = await _aPI_Application_V1Context.DanThuocModels.Where(o =>
            //o.UserId == userId && 
            o.AppID == appID!.AppID &&
            o.StudentId == studentId &&
            o.DocDate.Value.Day.ToString() == day &&
            o.DocDate.Value.Month.ToString() == month &&
            o.DocDate.Value.Year.ToString() == year
            ).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetDanThuocResponse { Success = true, DanThuocModels = danThuoc.ToList() };
        }
    }
}
