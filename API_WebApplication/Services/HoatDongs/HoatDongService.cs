using API_WebApplication.Interfaces.HoatDongs;
using API_WebApplication.Models;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.HoatDong;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.HoatDongs
{
    public class HoatDongService : IHoatDongService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public HoatDongService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteHoatDongResponse> DeleteHoatDong(int HoatDongId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var HoatDong = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.Id == HoatDongId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var HoatDong = await _aPI_Application_V1Context.HoatDongModels.FindAsync(HoatDongId);
            if (HoatDong == null)
            {
                return new DeleteHoatDongResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (HoatDong.UserId != userId)
            {
                return new DeleteHoatDongResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.HoatDongModels.Remove(HoatDong);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteHoatDongResponse
                {
                    Success = true,
                    HoatDongId = HoatDong.Id
                };
            }
            return new DeleteHoatDongResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetHoatDongResponse2> GetHoatDongByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var HoatDong = await _aPI_Application_V1Context.HoatDongModels
                .Where(o => o.AppID == appID!.AppID)
                .Select(o => new HoatDongModel2
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    AppID = o.AppID,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    IsCompleted = o.IsCompleted,
                    Img = o.Img,
                    ClassID = o.ClassID,
                    Months = o.Months,
                    Content = o.Content,
                    Years = o.Years,
                    Classs = _aPI_Application_V1Context.ClassModels.Where(x => x.Id == o.ClassID).FirstOrDefault(),
                    KhoaHocId = o.KhoaHocId,
                    KhoaHoc = _aPI_Application_V1Context.KhoaHocModels.Where(x => x.Id == o.KhoaHocId).FirstOrDefault()
                })
                .OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetHoatDongResponse2 { Success = true, HoatDongModels = HoatDong.ToList() };
        }

        public async Task<GetHoatDongResponse2> GetStudentsFillterByUserKhoaHocClass(int userId, string khoaHocId, string classId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.HoatDongModels
                .Where(
                o =>
                //o.UserId == userId && 
                o.KhoaHocId.ToString() == khoaHocId && o.ClassID.ToString() == classId && o.AppID == appID!.AppID
                )
                .Select(o => new HoatDongModel2
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    AppID = o.AppID,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    IsCompleted = o.IsCompleted,
                    Img = o.Img,
                    ClassID = o.ClassID,
                    Months = o.Months,
                    Content = o.Content,
                    Years = o.Years,
                    Classs = _aPI_Application_V1Context.ClassModels.Where(x => x.Id == o.ClassID).FirstOrDefault(),
                    KhoaHocId = o.KhoaHocId,
                    KhoaHoc = _aPI_Application_V1Context.KhoaHocModels.Where(x => x.Id == o.KhoaHocId).FirstOrDefault()
                }).OrderByDescending(o => o.CreateDate)
                .ToListAsync();
            return new GetHoatDongResponse2 { Success = true, HoatDongModels = student.ToList() };
        }

        public async Task<HoatDongResponse> GetIDHoatDong(int userId, int HoatDongId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _HoatDong = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.Id == HoatDongId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var _HoatDong = await _aPI_Application_V1Context.HoatDongModels.FindAsync(HoatDongId);
            if (_HoatDong == null)
            {
                return new HoatDongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_HoatDong.UserId != userId)
            {
                return new HoatDongResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_HoatDong.Id < 0)
            {
                return new HoatDongResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HoatDongResponse
            {
                Success = true,
                HoatDong = _HoatDong
            };
        }
        #region Phụ huynh
        public async Task<GetHoatDongResponse> PH_GetHoatDongBy(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var HoatDong = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.AppID == appID!.AppID).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetHoatDongResponse { Success = true, HoatDongs = HoatDong.ToList() };
        }
        #endregion

        #region Giáo viên
        public async Task<GetHoatDongResponse> GV_GetHoatDongByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var HoatDong = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.AppID == appID!.AppID).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetHoatDongResponse { Success = true, HoatDongs = HoatDong.ToList() };
        }
        #endregion

        public async Task<SaveHoatDongResponse> SaveHoatDong(HoatDongModel HoatDong)
        {
            await _aPI_Application_V1Context.HoatDongModels.AddAsync(HoatDong);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveHoatDongResponse
                {
                    Success = true,
                    HoatDong = HoatDong
                };
            }
            return new SaveHoatDongResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHoatDongResponse> UpdateHoatDong(int HoatDongId, HoatDongModel HoatDong)
        {
            var HoatDongById = await _aPI_Application_V1Context.HoatDongModels.FindAsync(HoatDongId);
            if (HoatDongById == null)
            {
                return new UpdateHoatDongResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            //if (HoatDong.UserId != HoatDongById.UserId)
            //{
            //    return new UpdateHoatDongResponse
            //    {
            //        Success = false,
            //        Error = "You don't have access to get id this ThoiKhoaBieu",
            //        ErrorCode = "T02"
            //    };
            //}

            HoatDongById.Content = HoatDong.Content;
            HoatDongById.UpdateDate = HoatDong.UpdateDate;
            HoatDongById.IsCompleted = HoatDong.IsCompleted;
            HoatDongById.AppID = HoatDong.AppID;
            HoatDongById.Months = HoatDong.Months;
            HoatDongById.Years = HoatDong.Years;
            HoatDongById.ClassID = HoatDong.ClassID;
            HoatDongById.KhoaHocId = HoatDong.KhoaHocId;
            HoatDongById.Img = HoatDong.Img;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHoatDongResponse
                {
                    Success = true,
                    HoatDong = HoatDong
                };
            }
            return new UpdateHoatDongResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<string> UploadFile([FromForm] UploadFileStudentModel fileStudentModel, string idUser)
        {
            string FileName = "";
            try
            {
                //FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                //FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Name;
                string root = @"upload\pdf" + idUser;
                // If directory does not exist, create it.
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string patch = Path.Combine(root, fileStudentModel.FileName);

                using (Stream stream = new FileStream(patch, FileMode.Create))
                {
                    await fileStudentModel.file.CopyToAsync(stream);
                    stream.Close();

                }
                return patch;// +fileStudentModel.FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<string> UploadFile(IFormFile _IFormFile, string idUser)
        //{
        //    string FileName = "";
        //    try
        //    {
        //        FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
        //        FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Name;
        //        string root = @"upload\pdf" + idUser;
        //        // If directory does not exist, create it.
        //        if (!Directory.Exists(root))
        //        {
        //            Directory.CreateDirectory(root);
        //        }
        //        string patch = Path.Combine(root, FileName);

        //        using (Stream stream = new FileStream(patch, FileMode.Create))
        //        {
        //            await _IFormFile.CopyToAsync(stream);
        //            stream.Close();

        //        }
        //        return FileName;
        //    } catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<(byte[], string, string)> DownloadFile(string FileName) {
            try
            {
                var _GetFilePatch = GetFilePatch(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if(!provider.TryGetContentType(_GetFilePatch, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePatch);
                return (_ReadAllBytesAsync,_ContentType,Path.GetFileName(_GetFilePatch));
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetStatusContentDirectory()
        {
            var result = Path.Combine(Directory.GetCurrentDirectory(),"upload\\StaticContent\\");
            if (Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;
        }
        public static string GetFilePatch(string FileName)
        {
            var _GetStatusContentDirectory = GetStatusContentDirectory();
            var result = Path.Combine(_GetStatusContentDirectory, FileName);
            return result;
        }
        #region Giáo viên
        public async Task<HoatDongResponse> GV_GetHoatDongByUserId(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var phanCongGiaoVienId = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o => o.UserAdd == appID.Id && o.AppID == appID.AppID && o.Status ==true).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
            var phanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.FindAsync(phanCongGiaoVienId.Id);
            var hocPhi = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.ClassID == phanCongGiaoVien!.ClassId && o.KhoaHocId == phanCongGiaoVien.KhoaHocId)
                .Select(pcu => new HoatDongModel
                {
                    Id = pcu.Id,
                    Content = pcu.Content,
                    IsCompleted = pcu.IsCompleted,
                    CreateDate = pcu.CreateDate,
                    UpdateDate = pcu.UpdateDate,
                    Months = pcu.Months,
                    Years = pcu.Years,
                    Img = pcu.Img,
                    AppID = pcu.AppID,
                    ClassID = pcu.ClassID,
                    KhoaHocId = pcu.KhoaHocId,

                }).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
            return new HoatDongResponse { Success = true, HoatDong = hocPhi };
        }
        #endregion

        #region Phụ huynh    
        public async Task<HoatDongResponse> PH_GetHoatDongByUserId(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            //var phanCongGiaoVienId = await _aPI_Application_V1Context.PhanCongGiaoViens.Where(o => o.UserAdd == appID.Id && o.AppID == appID.AppID && o.Status == true).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
            //var phanCongGiaoVien = await _aPI_Application_V1Context.PhanCongGiaoViens.FindAsync(phanCongGiaoVienId.Id);
            var phanCongGiaoVien = await _aPI_Application_V1Context.Students.Where(o => o.Id == appID.StudentId && o.AppID == appID.AppID).FirstOrDefaultAsync();
            var hocPhi = await _aPI_Application_V1Context.HoatDongModels.Where(o => o.ClassID.ToString() == phanCongGiaoVien.Class1 && o.KhoaHocId.ToString() == phanCongGiaoVien.Year1)
                .Select(pcu => new HoatDongModel
                {
                    Id = pcu.Id,
                    Content = pcu.Content,
                    IsCompleted = pcu.IsCompleted,
                    CreateDate = pcu.CreateDate,
                    UpdateDate = pcu.UpdateDate,
                    Months = pcu.Months,
                    Years = pcu.Years,
                    Img = pcu.Img,
                    AppID = pcu.AppID,
                    ClassID = pcu.ClassID,
                    KhoaHocId = pcu.KhoaHocId,

                }).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
            return new HoatDongResponse { Success = true, HoatDong = hocPhi };
        }
        #endregion
    }
}
