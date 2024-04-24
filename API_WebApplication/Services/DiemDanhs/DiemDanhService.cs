using API_WebApplication.Interfaces.DiemDanhs;
using API_WebApplication.Models;
using API_WebApplication.Requests.DiemDanh;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.DinhDuong;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace API_WebApplication.Services.DiemDanhs
{
    public class DiemDanhService : IDiemDanhService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public DiemDanhService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }      
        public async Task<DeleteDiemDanhResponse> DeleteDiemDanh(int DiemDanhId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => o.Id == DiemDanhId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var diemdanh = await _aPI_Application_V1Context.DiemDanhModels.FindAsync(DiemDanhId);
            if (diemdanh == null)
            {
                return new DeleteDiemDanhResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (diemdanh.UserId != userId)
            {
                return new DeleteDiemDanhResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.DiemDanhModels.Remove(diemdanh);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteDiemDanhResponse
                {
                    Success = true,
                    DiemDanhId = diemdanh.Id
                };
            }
            return new DeleteDiemDanhResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetDiemDanhResponse> GetDiemDanhByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => o.UserId == userId && o.AppID == appID!.AppID).ToListAsync();
            return new GetDiemDanhResponse { Success = true, DiemDanhs = diemdanh.ToList() };
        }
        public async Task<GetDiemDanhResponse> GetDiemDanhStatus(int userId, string day, string month, string year)
        {
            DateTime dateTime = DateTime.Now;
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => 
                o.AppID == appID!.AppID
                && o.CreateDate.Day.ToString() == day
                && o.CreateDate.Month.ToString() == month
                && o.CreateDate.Year.ToString() == year
            ).ToListAsync();
            return new GetDiemDanhResponse { Success = true, DiemDanhs = diemdanh.ToList() };
        }

        public async Task<DiemDanhResponse> GetDiemDanhByDateAndIDStudent(int studentId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();

            DateTime dateTime = DateTime.Now;
            var _diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => o.StudentId == studentId
                && o.AppID == appID!.AppID
                && o.CreateDate.Day == dateTime.Day
                && o.CreateDate.Month == dateTime.Month
                && o.CreateDate.Year == dateTime.Year
            ).FirstOrDefaultAsync();
            if (_diemdanh == null)
            {
                return new DiemDanhResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_diemdanh.Id < 0)
            {
                return new DiemDanhResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DiemDanhResponse
            {
                Success = true,
                DiemDanh = _diemdanh
            };
        }

        public async Task<DiemDanhResponse> GetIDDiemDanh(int userId, int DiemDanhId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => o.Id == DiemDanhId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _diemdanh = await _aPI_Application_V1Context.DiemDanhModels.FindAsync(DiemDanhId);
            if (_diemdanh == null)
            {
                return new DiemDanhResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_diemdanh.UserId != userId)
            {
                return new DiemDanhResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_diemdanh.Id < 0)
            {
                return new DiemDanhResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new DiemDanhResponse
            {
                Success = true,
                DiemDanh = _diemdanh
            };
        }

        public async Task<SaveDiemDanhResponse> SaveDiemDanh(DiemDanhModel DiemDanh)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == DiemDanh.UserId).FirstOrDefaultAsync();
            var _checkVars = await _aPI_Application_V1Context.DiemDanhModels.Where(o =>
                o.StudentId == DiemDanh.StudentId 
                && o.CreateDate.Day == DiemDanh.CreateDate.Day
                && o.CreateDate.Month ==DiemDanh.CreateDate.Month
                && o.CreateDate.Year == DiemDanh.CreateDate.Year && o.AppID == appID!.AppID
                ).FirstOrDefaultAsync();

            if (_checkVars != null)
            {
                return new SaveDiemDanhResponse
                {
                    Success = false,
                    Error = "Bạn đã điểm danh rồi",
                    ErrorCode = "T06"
                };
            }

            await _aPI_Application_V1Context.DiemDanhModels.AddAsync(DiemDanh);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveDiemDanhResponse
                {
                    Success = true,
                    DiemDanh = DiemDanh
                };
            }
            return new SaveDiemDanhResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<SaveDiemDanhResponse> SaveDiemDanhMultiple(List<DiemDanhModel> dataDiemDanh, int UserId)
        {
            List<DiemDanhModel> _diemDanh = new List<DiemDanhModel>();
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == UserId).FirstOrDefaultAsync();
            for (int i = 0;i < dataDiemDanh.Count;i++)
            {
                var _checkValue = await _aPI_Application_V1Context.DiemDanhModels.Where(o =>
                o.StudentId == dataDiemDanh[i].StudentId
                && o.CreateDate.Day == dataDiemDanh[i].CreateDate.Day
                && o.CreateDate.Month == dataDiemDanh[i].CreateDate.Month
                && o.CreateDate.Year == dataDiemDanh[i].CreateDate.Year && o.AppID == appID!.AppID
                ).FirstOrDefaultAsync();

                if (_checkValue == null)
                {
                    _diemDanh.Add(dataDiemDanh[i]);
                    await _aPI_Application_V1Context.DiemDanhModels.AddAsync(dataDiemDanh[i]);
                }
            }

            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveDiemDanhResponse
                {
                    Success = true,
                    DiemDanh = _diemDanh.LastOrDefault(),
                };
            }
            return new SaveDiemDanhResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateDiemDanhResponse> UpdateDiemDanh(int DiemDanhId, DiemDanhModel DiemDanh)
        {
            var diemDanhById = await _aPI_Application_V1Context.DiemDanhModels.FindAsync(DiemDanhId);
            if (diemDanhById == null)
            {
                return new UpdateDiemDanhResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (DiemDanh.UserId != diemDanhById.UserId)
            {
                return new UpdateDiemDanhResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            diemDanhById.StudentId = DiemDanh.StudentId;
            diemDanhById.Content = DiemDanh.Content;
            diemDanhById.DenLop = DiemDanh.DenLop;
            diemDanhById.CoPhep = DiemDanh.CoPhep;
            diemDanhById.KhongPhep = DiemDanh.KhongPhep;
            diemDanhById.UpdateDate = DiemDanh.UpdateDate;
            diemDanhById.IsCompleted = DiemDanh.IsCompleted;
            diemDanhById.AppID = DiemDanh.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateDiemDanhResponse
                {
                    Success = true,
                    DiemDanh = DiemDanh
                };
            }
            return new UpdateDiemDanhResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<GetDiemDanhResponse> UpdateDiemDanhMultiple(int UserId,List<DiemDanhRequest> dataDiemDanh)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == UserId).FirstOrDefaultAsync();

            for (int i = 0; i < dataDiemDanh.Count; i++)
            {
                var existingRecord = await _aPI_Application_V1Context.DiemDanhModels
                    .FirstOrDefaultAsync(d => d.CreateDate.Date == DateTime.UtcNow.Date && d.StudentId == dataDiemDanh[i].StudentId);

                if (appID == null)
                {
                    return new GetDiemDanhResponse
                    {
                        Success = false,
                        Error = "User not found",
                        ErrorCode = "T01"
                    };
                }
                if (existingRecord == null)
                {
                    // Nếu không có bản ghi tương tự, tạo mới
                    var newDiemDanh = new DiemDanhModel
                    {
                        UserId = dataDiemDanh[i].IdUser,
                        StudentId = dataDiemDanh[i].StudentId,
                        Content = dataDiemDanh[i].Content,
                        DenLop = dataDiemDanh[i].DenLop,
                        CoPhep = dataDiemDanh[i].CoPhep,
                        KhongPhep = dataDiemDanh[i].KhongPhep,
                        UpdateDate = dataDiemDanh[i].UpdateDate,
                        IsCompleted = dataDiemDanh[i].IsCompleted,
                        AppID = dataDiemDanh[i].AppID,
                        CreateDate = DateTime.UtcNow
                    };
                  
                    _aPI_Application_V1Context.DiemDanhModels.Add(newDiemDanh);
                }
                else
                {
                    UserId = dataDiemDanh[i].IdUser;
                    existingRecord.StudentId = dataDiemDanh[i].StudentId;
                    existingRecord.Content = dataDiemDanh[i].Content;
                    existingRecord.DenLop = dataDiemDanh[i].DenLop;
                    existingRecord.CoPhep = dataDiemDanh[i].CoPhep;
                    existingRecord.KhongPhep = dataDiemDanh[i].KhongPhep;
                    existingRecord.UpdateDate = dataDiemDanh[i].UpdateDate;
                    existingRecord.IsCompleted = dataDiemDanh[i].IsCompleted;
                    existingRecord.AppID = dataDiemDanh[i].AppID;

                }
            }

            await _aPI_Application_V1Context.SaveChangesAsync();
            return new GetDiemDanhResponse
            {
                Success = true
            };
        }
    }
}
