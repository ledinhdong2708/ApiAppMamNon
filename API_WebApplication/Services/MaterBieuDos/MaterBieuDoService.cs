using API_WebApplication.Interfaces.MaterBieuDos;
using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.MaterBieuDos;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.MaterBieuDos
{
    public class MaterBieuDoService : IMaterBieuDoService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public MaterBieuDoService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteMaterBieuDoResponse> DeleteMaterBieuDo(int MaterBieuDoId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.Where(o => o.Id == MaterBieuDoId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.FindAsync(MaterBieuDoId);
            if (materBieuDo == null)
            {
                return new DeleteMaterBieuDoResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (materBieuDo.UserId != userId)
            {
                return new DeleteMaterBieuDoResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.MaterBieuDos.Remove(materBieuDo);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteMaterBieuDoResponse
                {
                    Success = true,
                    MaterBieuDoId = materBieuDo.Id
                };
            }
            return new DeleteMaterBieuDoResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<MaterBieuDoResponse> GetIDMaterBieuDo(int userId, int MaterBieuDoId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.Where(o=>o.Id == MaterBieuDoId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.FindAsync(MaterBieuDoId);
            if (_materBieuDo == null)
            {
                return new MaterBieuDoResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_materBieuDo.UserId != userId)
            {
                return new MaterBieuDoResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_materBieuDo.Id < 0)
            {
                return new MaterBieuDoResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new MaterBieuDoResponse
            {
                Success = true,
                MaterBieuDo = _materBieuDo
            };
        }

        public async Task<GetMaterBieuDoResponse> GetMaterBieuDoByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.Where(o => o.UserId == userId && o.AppID == appID!.AppID).ToListAsync();
            return new GetMaterBieuDoResponse { Success = true, MaterBieuDos = materBieuDo.ToList() };
        }
        public async Task<GetMaterBieuDoResponse> GetMaterBieuDoByStudent(int userId, int studentId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.Where(o =>o.StudentId == studentId && o.AppID == appID!.AppID).ToListAsync();
            return new GetMaterBieuDoResponse { Success = true, MaterBieuDos = materBieuDo.ToList() };
        }

        

        public async Task<SaveMaterBieuDoResponse> SaveMaterBieuDo(MaterBieuDo MaterBieuDo)
        {
            await _aPI_Application_V1Context.MaterBieuDos.AddAsync(MaterBieuDo);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveMaterBieuDoResponse
                {
                    Success = true,
                    MaterBieuDo = MaterBieuDo
                };
            }
            return new SaveMaterBieuDoResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateMaterBieuDoResponse> UpdateMaterBieuDo(int MaterBieuDoId, MaterBieuDo MaterBieuDo)
        {
            var materBieuDoById = await _aPI_Application_V1Context.MaterBieuDos.FindAsync(MaterBieuDoId);
            if (materBieuDoById == null)
            {
                return new UpdateMaterBieuDoResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (MaterBieuDo.UserId != materBieuDoById.UserId)
            {
                return new UpdateMaterBieuDoResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            materBieuDoById.DocDate = MaterBieuDo.DocDate;
            materBieuDoById.CanNang = MaterBieuDo.CanNang;
            materBieuDoById.ChieuCao = MaterBieuDo.ChieuCao;
            materBieuDoById.BMI = MaterBieuDo.BMI;
            materBieuDoById.StudentId = MaterBieuDo.StudentId;
            materBieuDoById.UpdateDate = MaterBieuDo.UpdateDate;
            materBieuDoById.IsCompleted = MaterBieuDo.IsCompleted;
            materBieuDoById.AppID = MaterBieuDo.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateMaterBieuDoResponse
                {
                    Success = true,
                    MaterBieuDo = MaterBieuDo
                };
            }
            return new UpdateMaterBieuDoResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetMaterBieuDoResponse> PH_GetMaterBieuDoByStudent(int userId,int id)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var materBieuDo = await _aPI_Application_V1Context.MaterBieuDos.Where(o => o.StudentId == id && o.AppID == appID!.AppID).ToListAsync();
            return new GetMaterBieuDoResponse { Success = true, MaterBieuDos = materBieuDo.ToList() };
        }
    }
}
