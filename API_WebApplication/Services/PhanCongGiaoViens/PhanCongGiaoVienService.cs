using API_WebApplication.Interfaces.PhanCongGiaoVienModels;
using API_WebApplication.Models;
using API_WebApplication.Responses.PhanCongGiaoViens;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.PhanCongGiaoViens
{
    public class PhanCongGiaoVienService: IPhanCongGiaoVienService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public PhanCongGiaoVienService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeletePhanCongGiaoVienResponse> DeletePhanCongGiaoVien(int PhanCongGiaoVienId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o => o.Id == PhanCongGiaoVienId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoVienModels.FindAsync(PhanCongGiaoVienId);
            if (PhanCongGiaoVien == null)
            {
                return new DeletePhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (PhanCongGiaoVien.UserId != userId)
            {
                return new DeletePhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.PhanCongGiaoViens.Remove(PhanCongGiaoVien);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeletePhanCongGiaoVienResponse
                {
                    Success = true,
                    PhanCongGiaoVienId = PhanCongGiaoVien.Id
                };
            }
            return new DeletePhanCongGiaoVienResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetPhanCongGiaoVienResponse> GetPhanCongGiaoVienByStudent(int userId, int id)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o =>
            //o.UserId == userId && 
            o.AppID == appID!.AppID &&
            o.UserAdd == id
            //o.ClassId == studentId &&
            ).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetPhanCongGiaoVienResponse { Success = true, PhanCongGiaoViens = PhanCongGiaoVien.ToList() };
        }

        public async Task<GetPhanCongGiaoVienResponse> GetPhanCongGiaoVienByUser(int userId, string classid, string khoahocid)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens
                .Where(o => o.UserId == userId && o.AppID == appID!.AppID && o.ClassId.ToString() == classid && o.KhoaHocId.ToString() == khoahocid
            ).ToListAsync();
            return new GetPhanCongGiaoVienResponse { Success = true, PhanCongGiaoViens = PhanCongGiaoVien.ToList() };
        }

        public async Task<PhanCongGiaoVienResponse> GetIDPhanCongGiaoVien(int userId, int PhanCongGiaoVienId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o => o.Id == PhanCongGiaoVienId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoVienModels.FindAsync(PhanCongGiaoVienId);
            if (_PhanCongGiaoVien == null)
            {
                return new PhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_PhanCongGiaoVien.UserId != userId)
            {
                return new PhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_PhanCongGiaoVien.Id < 0)
            {
                return new PhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new PhanCongGiaoVienResponse
            {
                Success = true,
                PhanCongGiaoVien = _PhanCongGiaoVien
            };
        }

        public async Task<SavePhanCongGiaoVienResponse> SavePhanCongGiaoVien(PhanCongGiaoVienModel PhanCongGiaoVien)
        {
            await _aPI_Application_V1Context.PhanCongGiaoViens.AddAsync(PhanCongGiaoVien);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SavePhanCongGiaoVienResponse
                {
                    Success = true,
                    PhanCongGiaoVien = PhanCongGiaoVien
                };
            }
            return new SavePhanCongGiaoVienResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<SavePhanCongGiaoVienResponse> PH_SavePhanCongGiaoVien(PhanCongGiaoVienModel PhanCongGiaoVien)
        {
            await _aPI_Application_V1Context.PhanCongGiaoViens.AddAsync(PhanCongGiaoVien);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SavePhanCongGiaoVienResponse
                {
                    Success = true,
                    PhanCongGiaoVien = PhanCongGiaoVien
                };
            }
            return new SavePhanCongGiaoVienResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdatePhanCongGiaoVienResponse> UpdatePhanCongGiaoVien(int PhanCongGiaoVienId, PhanCongGiaoVienModel PhanCongGiaoVien)
        {
            var PhanCongGiaoVienById = await _aPI_Application_V1Context.PhanCongGiaoViens.FindAsync(PhanCongGiaoVienId);
            if (PhanCongGiaoVienById == null)
            {
                return new UpdatePhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (PhanCongGiaoVien.UserId != PhanCongGiaoVienById.UserId)
            {
                return new UpdatePhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            PhanCongGiaoVienById.Content = PhanCongGiaoVien.Content;
            PhanCongGiaoVienById.ClassId = PhanCongGiaoVien.ClassId;
            PhanCongGiaoVienById.KhoaHocId = PhanCongGiaoVien.KhoaHocId;
            PhanCongGiaoVienById.UpdateDate = PhanCongGiaoVien.UpdateDate;
            PhanCongGiaoVienById.Status = PhanCongGiaoVien.Status;
            PhanCongGiaoVienById.AppID = PhanCongGiaoVien.AppID;
            PhanCongGiaoVienById.UserAdd = PhanCongGiaoVien.UserAdd;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdatePhanCongGiaoVienResponse
                {
                    Success = true,
                    PhanCongGiaoVien = PhanCongGiaoVien
                };
            }
            return new UpdatePhanCongGiaoVienResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdatePhanCongGiaoVienResponse> PH_UpdatePhanCongGiaoVien(int PhanCongGiaoVienId, PhanCongGiaoVienModel PhanCongGiaoVien)
        {
            var PhanCongGiaoVienById = await _aPI_Application_V1Context.PhanCongGiaoViens.FindAsync(PhanCongGiaoVienId);
            if (PhanCongGiaoVienById == null)
            {
                return new UpdatePhanCongGiaoVienResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }

            PhanCongGiaoVienById.Content = PhanCongGiaoVien.Content;
            PhanCongGiaoVienById.ClassId = PhanCongGiaoVien.ClassId;
            PhanCongGiaoVienById.UpdateDate = PhanCongGiaoVien.UpdateDate;
            PhanCongGiaoVienById.AppID = PhanCongGiaoVien.AppID;
            PhanCongGiaoVienById.Status = PhanCongGiaoVien.Status;
            PhanCongGiaoVienById.UserAdd = PhanCongGiaoVien.UserAdd;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdatePhanCongGiaoVienResponse
                {
                    Success = true,
                    PhanCongGiaoVien = PhanCongGiaoVien
                };
            }
            return new UpdatePhanCongGiaoVienResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetPhanCongGiaoVienResponse> PH_GetPhanCongGiaoVienByStudent(int userId, int classid)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var PhanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o =>
            //o.UserId == userId && 
            o.AppID == appID!.AppID &&
            o.ClassId == classid 
            ).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetPhanCongGiaoVienResponse { Success = true, PhanCongGiaoViens = PhanCongGiaoVien.ToList() };
        }
    }
}
