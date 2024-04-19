using API_WebApplication.Interfaces.MaterHocPhis;
using API_WebApplication.Models;
using API_WebApplication.Responses.MaterBieuDos;
using API_WebApplication.Responses.MaterHocPhis;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.MaterHocPhis
{
    public class MaterHocPhiService : IMaterHocPhiService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public MaterHocPhiService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteMaterHocPhiResponse> DeleteMaterHocPhi(int MaterHocPhiId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materHocPhi = await _aPI_Application_V1Context.MaterHocPhis
                .Where(o => o.Id == MaterHocPhiId && o.AppID == appID!.AppID)
                .FirstOrDefaultAsync();
            if (materHocPhi == null)
            {
                return new DeleteMaterHocPhiResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (materHocPhi.UserId != userId)
            {
                return new DeleteMaterHocPhiResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.MaterHocPhis.Remove(materHocPhi);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteMaterHocPhiResponse
                {
                    Success = true,
                    MaterHocPhiId = materHocPhi.Id
                };
            }
            return new DeleteMaterHocPhiResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<MaterHocPhiResponse> GetIDMaterHocPhi(int userId, int MaterHocPhiId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _materHocPhi = await _aPI_Application_V1Context.MaterHocPhis
                .Where(o => o.Id == MaterHocPhiId && o.AppID == appID!.AppID)
                .FirstOrDefaultAsync();
            if (_materHocPhi == null)
            {
                return new MaterHocPhiResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_materHocPhi.UserId != userId)
            {
                return new MaterHocPhiResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_materHocPhi.Id < 0)
            {
                return new MaterHocPhiResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new MaterHocPhiResponse
            {
                Success = true,
                MaterHocPhi = _materHocPhi
            };
        }

        public async Task<GetMaterHocPhiResponse> GetMaterHocPhiByStudent(int userId, int studentId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materHocPhi = await _aPI_Application_V1Context.MaterHocPhis
                .Where(o => o.UserId == userId && o.StudentId == studentId && o.AppID == appID!.AppID)
                .ToListAsync();
            return new GetMaterHocPhiResponse { Success = true, MaterHocPhis = materHocPhi.ToList() };
        }

        public async Task<GetMaterHocPhiResponse> GetMaterHocPhiByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materHocPhi = await _aPI_Application_V1Context.MaterHocPhis.Where(o => o.UserId == userId && o.AppID == appID!.AppID).ToListAsync();
            return new GetMaterHocPhiResponse { Success = true, MaterHocPhis = materHocPhi.ToList() };
        }
        public async Task<GetMaterHocPhiResponse> GetMaterHocPhiFalseByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materHocPhi = await _aPI_Application_V1Context.MaterHocPhis.Where(o => o.UserId == userId && o.AppID == appID!.AppID && o.IsCompleted == false).ToListAsync();
            return new GetMaterHocPhiResponse { Success = true, MaterHocPhis = materHocPhi.ToList() };
        }

        public async Task<GetMaterHocPhiResponse> PH_GetMaterHocPhiByStudent(int id, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materHocPhi = await _aPI_Application_V1Context.MaterHocPhis.Where(o => o.StudentId == id && o.AppID == appID!.AppID).ToListAsync();
            return new GetMaterHocPhiResponse { Success = true, MaterHocPhis = materHocPhi.ToList() };
        }

        public async Task<SaveMaterHocPhiResponse> SaveMaterHocPhi(MaterHocPhi MaterHocPhi)
        {
            await _aPI_Application_V1Context.MaterHocPhis.AddAsync(MaterHocPhi);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveMaterHocPhiResponse
                {
                    Success = true,
                    MaterHocPhi = MaterHocPhi
                };
            }
            return new SaveMaterHocPhiResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateMaterHocPhiResponse> UpdateMaterHocPhi(int MaterHocPhiId, MaterHocPhi MaterHocPhi)
        {
            var materHocPhiById = await _aPI_Application_V1Context.MaterHocPhis.FindAsync(MaterHocPhiId);
            if (materHocPhiById == null)
            {
                return new UpdateMaterHocPhiResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (MaterHocPhi.UserId != materHocPhiById.UserId)
            {
                return new UpdateMaterHocPhiResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            materHocPhiById.Content = MaterHocPhi.Content;
            materHocPhiById.DonViTinh = MaterHocPhi.DonViTinh;
            materHocPhiById.StudentId = MaterHocPhi.StudentId;
            materHocPhiById.UpdateDate = MaterHocPhi.UpdateDate;
            materHocPhiById.IsCompleted = MaterHocPhi.IsCompleted;
            materHocPhiById.AppID = MaterHocPhi.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateMaterHocPhiResponse
                {
                    Success = true,
                    MaterHocPhi = MaterHocPhi
                };
            }
            return new UpdateMaterHocPhiResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
