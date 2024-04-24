using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.DinhDuongs
{
    public class DinhDuongService : IDinhDuongService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public DinhDuongService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }
        public async Task<DeleteDinhDuongResponse> DeleteDinhDuong(int dinhduongId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var dinhduong = await _aPI_Application_V1Context.DinhDuongModels.Where(o => o.Id == dinhduongId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var dinhduong = await _aPI_Application_V1Context.DinhDuongModels.FindAsync(dinhduongId);
            if (dinhduong == null)
            {
                return new DeleteDinhDuongResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (dinhduong.UserId != userId)
            {
                return new DeleteDinhDuongResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.DinhDuongModels.Remove(dinhduong);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteDinhDuongResponse
                {
                    Success = true,
                     DinhDuongId = dinhduong.Id
                };
            }
            return new DeleteDinhDuongResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<DinhDuongResponse> GetIDDinhDuong(int userId, int dinhduongId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _dinhduong = await _aPI_Application_V1Context.DinhDuongModels.Where(o => o.Id == dinhduongId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _dinhduong = await _aPI_Application_V1Context.DinhDuongModels.FindAsync(dinhduongId);
            if (_dinhduong == null)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_dinhduong.UserId != userId)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_dinhduong.Id < 0)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DinhDuongResponse
            {
                Success = true,
                DinhDuong = _dinhduong
            };
        }

        public async Task<GetDinhDuongResponse> GetDinhDuongByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var dinhduong = await _aPI_Application_V1Context.DinhDuongModels.Where(o => o.UserId == userId && o.AppID == appID!.AppID).ToListAsync();
            return new GetDinhDuongResponse { Success = true, DinhDuongs = dinhduong.ToList() };
        }

        public async Task<SaveDinhDuongResponse> SaveDinhDuong(DinhDuongModel dinhduong)
        {
            await _aPI_Application_V1Context.DinhDuongModels.AddAsync(dinhduong);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveDinhDuongResponse
                {
                    Success = true,
                    DinhDuong = dinhduong
                };
            }
            return new SaveDinhDuongResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateDinhDuongResponse> UpdateDinhDuong(int dinhduongId, DinhDuongModel dinhduong)
        {
            var dinhduongById = await _aPI_Application_V1Context.DinhDuongModels.FindAsync(dinhduongId);
            if (dinhduongById == null)
            {
                return new UpdateDinhDuongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (dinhduong.UserId != dinhduongById.UserId)
            {
                return new UpdateDinhDuongResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            dinhduongById.DocDate = dinhduong.DocDate;
            dinhduongById.BuoiSang = dinhduong.BuoiSang;
            dinhduongById.BuoiTrua = dinhduong.BuoiTrua;
            dinhduongById.BuoiChinhChieu = dinhduong.BuoiChinhChieu;
            dinhduongById.BuoiPhuChieu = dinhduong.BuoiPhuChieu;
            dinhduongById.Dam = dinhduong.Dam;
            dinhduongById.DamDinhMuc = dinhduong.DamDinhMuc;
            dinhduongById.Beo = dinhduong.Beo;
            dinhduongById.BeoDinhMuc = dinhduong.BeoDinhMuc;
            dinhduongById.Duong = dinhduong.Duong;
            dinhduongById.DuongDinhMuc = dinhduong.DuongDinhMuc;
            dinhduongById.NangLuong = dinhduong.NangLuong;
            dinhduongById.NangLuongDinhMuc = dinhduong.NangLuongDinhMuc;
            dinhduongById.UpdateDate = dinhduong.UpdateDate;
            dinhduongById.IsCompleted = dinhduong.IsCompleted;
            dinhduongById.AppID = dinhduong.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateDinhDuongResponse
                {
                    Success = true,
                    DinhDuong = dinhduong
                };
            }
            return new UpdateDinhDuongResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<DinhDuongResponse> GetIDDinhDuongByDate(int userId, int day, int month, int year, string khoahocid, string classid)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _dinhduong = await _aPI_Application_V1Context.DinhDuongModels.Where(o => o.AppID == appID!.AppID &&
                o.DocDate.Value.Day == day && o.DocDate.Value.Month == month && o.DocDate.Value.Year == year
                && o.KhoaHocID.ToString() == khoahocid && o.ClassID.ToString() == classid
                ).FirstOrDefaultAsync();
            if (_dinhduong == null)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_dinhduong.UserId != userId)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_dinhduong.Id < 0)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DinhDuongResponse
            {
                Success = true,
                DinhDuong = _dinhduong
            };
        }
        public async Task<DinhDuongResponse> PH_GetIDDinhDuongByDate(int day, int month, int year, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => o.Id == appID!.StudentId).FirstOrDefaultAsync();
            var _dinhduong = await _aPI_Application_V1Context.DinhDuongModels.Where(o =>
                o.DocDate.Value.Day == day && o.DocDate.Value.Month == month && o.DocDate.Value.Year == year && o.AppID == appID!.AppID
                && o.ClassID.ToString() == student.Class1 && o.KhoaHocID.ToString() == student.Year1).FirstOrDefaultAsync();
            if (_dinhduong == null)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_dinhduong.Id < 0)
            {
                return new DinhDuongResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DinhDuongResponse
            {
                Success = true,
                DinhDuong = _dinhduong
            };
        }
    }
}
