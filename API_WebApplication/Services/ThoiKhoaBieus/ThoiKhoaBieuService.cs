using API_WebApplication.Interfaces.ThoiKhoaBieus;
using API_WebApplication.Models;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.ThoiKhoaBieus
{
    public class ThoiKhoaBieuService : IThoiKhoaBieuService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public ThoiKhoaBieuService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteThoiKhoaBieuResponse> DeleteThoiKhoaBieu(int thoikhoabieuId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var thoikhoabieu = await _aPI_Application_V1Context.Students.Where(o => o.Id == thoikhoabieuId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var thoikhoabieu = await _aPI_Application_V1Context.Students.FindAsync(thoikhoabieuId);
            if (thoikhoabieu == null)
            {
                return new DeleteThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (thoikhoabieu.UserId != userId)
            {
                return new DeleteThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.Students.Remove(thoikhoabieu);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteThoiKhoaBieuResponse
                {
                    Success = true,
                    ThoiKhoaBieuId = thoikhoabieu.Id
                };
            }
            return new DeleteThoiKhoaBieuResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }
        public async Task<ThoiKhoaBieuResponse> GetIDThoiKhoaBieu(int userId, int thoikhoabieuId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _thoikhoabieu = await _aPI_Application_V1Context.ThoiKhoaBieuModels.Where(o => o.Id == thoikhoabieuId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _thoikhoabieu = await _aPI_Application_V1Context.ThoiKhoaBieuModels.FindAsync(thoikhoabieuId);
            if (_thoikhoabieu == null)
            {
                return new ThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_thoikhoabieu.UserId != userId)
            {
                return new ThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_thoikhoabieu.Id < 0)
            {
                return new ThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new ThoiKhoaBieuResponse
            {
                Success = true,
                ThoiKhoaBieu = _thoikhoabieu
            };
        }
        public async Task<GetThoiKhoaBieuResponse> GetThoiKhoaBieuByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var thoikhoabieu = await _aPI_Application_V1Context.ThoiKhoaBieuModels
                .Where(o => o.UserId == userId && o.AppID == appID!.AppID)
                .Select(o => new ThoiKhoaBieuModel {
                    Id = o.Id,
                    IsCompleted = o.IsCompleted,
                    ClassTKB = o.ClassTKB,
                    NameTKB = o.NameTKB,
                    Command = o.Command,
                    Time06300720 = o.Time06300720,
                    Time07200730 = o.Time07200730,
                    Time07300815 = o.Time07300815,
                    Time08150845 = o.Time08150845,
                    Time08450900 = o.Time08450900,
                    Time09000930 = o.Time09000930,
                    Time09301015 = o.Time09301015,
                    Time10151115 = o.Time10151115,
                    Time11151400 = o.Time11151400,
                    Time14001415 = o.Time14001415,
                    Time14151500 = o.Time14151500,
                    Time15001515 = o.Time15001515,
                    Time15151540 = o.Time15151540,
                    Time15301630 = o.Time15301630,
                    Time16301730 = o.Time16301730,
                    Time17301815 = o.Time17301815,
                    CreateDate = o.CreateDate,
                    KhoaHocId = o.KhoaHocId,
                    days = o.days,
                    months = o.months,
                    years = o.years
                }).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetThoiKhoaBieuResponse { Success = true, ThoiKhoaBieus = thoikhoabieu.ToList() };
        }
        public async Task<SaveThoiKhoaBieuResponse> SaveThoiKhoaBieu(ThoiKhoaBieuModel thoiKhoaBieu)
        {
            await _aPI_Application_V1Context.ThoiKhoaBieuModels.AddAsync(thoiKhoaBieu);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveThoiKhoaBieuResponse
                {
                    Success = true,
                    ThoiKhoaBieu = thoiKhoaBieu
                };
            }
            return new SaveThoiKhoaBieuResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateThoiKhoaBieuResponse> UpdateThoiKhoaBieu(int thoikhoabieuId, ThoiKhoaBieuModel thoiKhoaBieu)
        {
            var thoikhoabieuById = await _aPI_Application_V1Context.ThoiKhoaBieuModels.FindAsync(thoikhoabieuId);
            if (thoikhoabieuById == null)
            {
                return new UpdateThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (thoiKhoaBieu.UserId != thoikhoabieuById.UserId)
            {
                return new UpdateThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            thoikhoabieuById.ClassTKB = thoiKhoaBieu.ClassTKB;
            thoikhoabieuById.NameTKB = thoiKhoaBieu.NameTKB;
            thoikhoabieuById.Command = thoiKhoaBieu.Command;
            thoikhoabieuById.Time06300720 = thoiKhoaBieu.Time06300720;
            thoikhoabieuById.Time07200730 = thoiKhoaBieu.Time07200730;
            thoikhoabieuById.Time07300815 = thoiKhoaBieu.Time07300815;
            thoikhoabieuById.Time08150845 = thoiKhoaBieu.Time08150845;
            thoikhoabieuById.Time08450900 = thoiKhoaBieu.Time08450900;
            thoikhoabieuById.Time09000930 = thoiKhoaBieu.Time09000930;
            thoikhoabieuById.Time09301015 = thoiKhoaBieu.Time09301015;
            thoikhoabieuById.Time10151115 = thoiKhoaBieu.Time10151115;
            thoikhoabieuById.Time11151400 = thoiKhoaBieu.Time11151400;
            thoikhoabieuById.Time14001415 = thoiKhoaBieu.Time14001415;
            thoikhoabieuById.Time14151500 = thoiKhoaBieu.Time14151500;
            thoikhoabieuById.Time15001515 = thoiKhoaBieu.Time15001515;
            thoikhoabieuById.Time15151540 = thoiKhoaBieu.Time15151540;
            thoikhoabieuById.Time15301630 = thoiKhoaBieu.Time15301630;
            thoikhoabieuById.Time16301730 = thoiKhoaBieu.Time16301730;
            thoikhoabieuById.Time17301815 = thoiKhoaBieu.Time17301815;
            thoikhoabieuById.UpdateDate = thoiKhoaBieu.UpdateDate;
            thoikhoabieuById.IsCompleted = thoiKhoaBieu.IsCompleted;
            thoikhoabieuById.KhoaHocId = thoiKhoaBieu.KhoaHocId;
            thoikhoabieuById.AppID = thoiKhoaBieu.AppID;
            thoikhoabieuById.days = thoiKhoaBieu.days;
            thoikhoabieuById.months = thoiKhoaBieu.months;
            thoikhoabieuById.years = thoiKhoaBieu.years;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateThoiKhoaBieuResponse
                {
                    Success = true,
                    ThoiKhoaBieu = thoiKhoaBieu
                };
            }
            return new UpdateThoiKhoaBieuResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetThoiKhoaBieuResponse> GetStudentsFillterByUserDateClass(int userId, string day, string month, string year, string classId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var thoikhoabieu = await _aPI_Application_V1Context.ThoiKhoaBieuModels.Where(o => o.UserId == userId 
            && o.days == day
            && o.months == month
            && o.years == year
            && o.ClassTKB == classId
            && o.AppID == appID!.AppID)
                .Select(o => new ThoiKhoaBieuModel {
                    Id = o.Id,
                    IsCompleted = o.IsCompleted,
                    ClassTKB = o.ClassTKB,
                    NameTKB = _aPI_Application_V1Context.ClassModels.Where(_=>_.Id.ToString() == o.ClassTKB).FirstOrDefault().NameClass + " ("
                    + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id == o.KhoaHocId).FirstOrDefault().FromYear
                    + " - "
                    + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id == o.KhoaHocId).FirstOrDefault().ToYear
                    + ")",
                    Command = o.Command,
                    Time06300720 = o.Time06300720,
                    Time07200730 = o.Time07200730,
                    Time07300815 = o.Time07300815,
                    Time08150845 = o.Time08150845,
                    Time08450900 = o.Time08450900,
                    Time09000930 = o.Time09000930,
                    Time09301015 = o.Time09301015,
                    Time10151115 = o.Time10151115,
                    Time11151400 = o.Time11151400,
                    Time14001415 = o.Time14001415,
                    Time14151500 = o.Time14151500,
                    Time15001515 = o.Time15001515,
                    Time15151540 = o.Time15151540,
                    Time15301630 = o.Time15301630,
                    Time16301730 = o.Time16301730,
                    Time17301815 = o.Time17301815,
                    CreateDate = o.CreateDate,
                    KhoaHocId = o.KhoaHocId,
                    days = o.days,
                    months = o.months,
                    years = o.years,
                }).OrderByDescending(o => o.CreateDate)
                .ToListAsync();
            return new GetThoiKhoaBieuResponse { Success = true, ThoiKhoaBieus = thoikhoabieu.ToList() };
        }
        public async Task<ThoiKhoaBieuResponse> PH_GetIDByStudent(string day, string month, string year,int id, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var studentId = await _aPI_Application_V1Context.Students.FindAsync(id);
            var _thoikhoabieu = await _aPI_Application_V1Context.ThoiKhoaBieuModels.Where(
                p => p.ClassTKB == studentId.Class1
                && p.KhoaHocId.ToString() == studentId.Year1.ToString()
                && p.AppID == appID!.AppID
                && p.days == day
                && p.months == month && p.years == year
                ).FirstOrDefaultAsync();
            if (_thoikhoabieu == null)
            {
                return new ThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_thoikhoabieu.Id < 0)
            {
                return new ThoiKhoaBieuResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new ThoiKhoaBieuResponse
            {
                Success = true,
                ThoiKhoaBieu = _thoikhoabieu
            };
        }
    }
}
