using API_WebApplication.DTO.HocPhiModel;
using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Interfaces.HocPhiModels;
using API_WebApplication.Models;
using API_WebApplication.Responses.HocPhiModel;
using API_WebApplication.Responses.HocPhis;
using API_WebApplication.Responses.Logins;
using API_WebApplication.Responses.MaterBieuDos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_WebApplication.Services.HocPhiModels
{
    public class HocPhiModelService : IHocPhiModelService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly IMapper _mapper;

        public HocPhiModelService(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            this._mapper = mapper;
        }

        public async Task<DeleteHocPhiModel2Response> DeleteHocPhiModel(int HocPhiModelId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = await _aPI_Application_V1Context.HocPhiModels.Where(o => o.Id == HocPhiModelId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (hocphi == null)
            {
                return new DeleteHocPhiModel2Response
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphi.UserId != userId)
            {
                return new DeleteHocPhiModel2Response
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.HocPhiModels.Remove(hocphi);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteHocPhiModel2Response
                {
                    Success = true,
                    HocPhiModelId = hocphi.Id
                };
            }
            return new DeleteHocPhiModel2Response
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetHocPhiModel2Response> GetHocPhiModelsByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocPhiModel = await _aPI_Application_V1Context.HocPhiModels
                .Where(_ => _.UserId == userId && _.AppID == appID!.AppID)
                .Include(_ => _.HocPhiChiTietModels)
                .OrderByDescending(o => o.CreateDate).ToListAsync();
            if (hocPhiModel.Count == 0)
            {
                return new GetHocPhiModel2Response
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04",
                    Data = hocPhiModel.ToList()
                };
            }
            return new GetHocPhiModel2Response { Success = true, HocPhiModels = hocPhiModel };
        }

        public async Task<HocPhiModel2Response> GetIDHocPhiModel(int userId, int HocPhiId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocPhiModel = await _aPI_Application_V1Context.HocPhiModels
                .Include(_ => _.HocPhiChiTietModels)
                .Where(_ => _.Id == HocPhiId && _.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (hocPhiModel == null)
            {
                return new HocPhiModel2Response
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (hocPhiModel.UserId != userId)
            {
                return new HocPhiModel2Response
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (hocPhiModel.Id < 0)
            {
                return new HocPhiModel2Response
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HocPhiModel2Response
            {
                Success = true,
                Data = hocPhiModel
            };
        }

        public async Task<SaveHocPhiModel2Response> SaveHocPhiModel(HocPhiModel2DTO _HocPhiModel)
        {
            var newTKB = _mapper.Map<HocPhiModel2>(_HocPhiModel);
            await _aPI_Application_V1Context.AddAsync(newTKB);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveHocPhiModel2Response
                {
                    Success = true,
                    Data = newTKB
                };
            }
            return new SaveHocPhiModel2Response
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHocPhiModel2Response> UpdateHocPhiModel(int HocPhiModelId, HocPhiModel2DTO _HocPhiModel)
        {
            var updateOTKB = _mapper.Map<HocPhiModel2>(_HocPhiModel);
            _aPI_Application_V1Context.Update(updateOTKB);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModel2Response
                {
                    Success = true,
                    HocPhiModel = _HocPhiModel
                };
            }
            return new UpdateHocPhiModel2Response
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHocPhiModel2_DetailResponse> UpdateHocPhiModel_Detail(int HocPhiModelId, HocPhiModel2 _HocPhiModel)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == _HocPhiModel.UserId).FirstOrDefaultAsync();
            var hocphiById = await _aPI_Application_V1Context.HocPhiModels.FindAsync(HocPhiModelId);

            if (hocphiById == null)
            {
                return new UpdateHocPhiModel2_DetailResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphiById.UserId != hocphiById.UserId)
            {
                return new UpdateHocPhiModel2_DetailResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }
            hocphiById.Content = _HocPhiModel.Content;
            hocphiById.UserId = _HocPhiModel.UserId;
            hocphiById.Months = _HocPhiModel.Months;
            hocphiById.Years = _HocPhiModel.Years;
            hocphiById.Total = _HocPhiModel.Total;
            hocphiById.PhiOff = _HocPhiModel.PhiOff;
            hocphiById.ConLai = _HocPhiModel.ConLai;
            hocphiById.IsCompleted = _HocPhiModel.IsCompleted;
            hocphiById.UpdateDate = _HocPhiModel.UpdateDate;
            hocphiById.Status = _HocPhiModel.Status;
            hocphiById.AppID = _HocPhiModel.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModel2_DetailResponse
                {
                    Success = true,
                    HocPhiModel = _HocPhiModel
                };
            }
            return new UpdateHocPhiModel2_DetailResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetHocPhiModel2Response> GetHocPhiModelByStudent(int userId, int studentId)
        {
            var appID = await _aPI_Application_V1Context.Users
                .Where(o => o.Id == userId)
                .Select(u => u.AppID)
                .FirstOrDefaultAsync();

            var hocPhiModels = await _aPI_Application_V1Context.HocPhiModels
                .Where(h => h.StudentId == studentId && h.AppID == appID).Include(x => x.HocPhiChiTietModels)
                .ToListAsync();
            var diemdanhInMonthYear = await _aPI_Application_V1Context.DiemDanhModels
                        .Where(s => s.StudentId == studentId)
                        .ToListAsync();

            var gia = await _aPI_Application_V1Context.GiaTiens.ToListAsync();

            if (studentId == null)
            {
                return new GetHocPhiModel2Response
                {
                    Success = false,
                    Error = "Error",
                    ErrorCode = "T05"
                };
            }
            decimal tongphioff = 0;
            if (gia != null)
            {
                foreach (var hocPhi in hocPhiModels)
                {
                    var totalOffDays = 0;
                    foreach (var sobuoi in diemdanhInMonthYear)
                    {
                        var monthYear = $"{sobuoi.CreateDate.Month}/{sobuoi.CreateDate.Year}";

                        if (monthYear == $"{hocPhi.Months}/{hocPhi.Years}" && sobuoi.DenLop != true)
                        {
                            totalOffDays++;
                        }

                    }
                    decimal totalPrice = 0;
                    foreach (var giatien in gia)
                    {
                        totalPrice += giatien.gia * totalOffDays;
                    }


                    hocPhi.Sobuoioff = totalOffDays;
                    tongphioff = totalPrice;
                    hocPhi.PhiOff = tongphioff;
                    hocPhi.ConLai = hocPhi.Total - hocPhi.PhiOff;

                }

            }




            await _aPI_Application_V1Context.SaveChangesAsync(); 

            var hocPhiDTOs = hocPhiModels.Select(hocPhi => new HocPhiModel2
            {
                Id = hocPhi.Id,
                StudentId = hocPhi.StudentId,
                AppID = appID,
                ConLai = hocPhi.ConLai,
                Content = hocPhi.Content,
                PhiOff = hocPhi.PhiOff,
                UpdateDate = hocPhi.UpdateDate,
                Status = hocPhi.Status,
                Total = hocPhi.Total,
                CreateDate = hocPhi.CreateDate,
                Months = hocPhi.Months,
                Years = hocPhi.Years,
                IsCompleted = hocPhi.IsCompleted,
                Sobuoioff = hocPhi.Sobuoioff,
                HocPhiChiTietModels = hocPhi.HocPhiChiTietModels.Select(c => new HocPhiChiTietModel2
                {

                    Id = c.Id,
                    StudentId = c.StudentId,
                    AppID = c.AppID,
                    HocPhiId = c.HocPhiId,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    Content = c.Content,
                    MaterHocPhiId = c.MaterHocPhiId,
                    Total = c.Total,
                    Quantity = c.Quantity,
                }).ToList()
            }).ToList();
            return new GetHocPhiModel2Response { Success = true, HocPhiModels = hocPhiDTOs };


        }
        public async Task<GetHocPhiModel2Response> PH_GetHocPhiModelByStudent(int userId, int studentId)
        {
            var appID = await _aPI_Application_V1Context.Users
                .Where(o => o.Id == userId)
                .Select(u => u.AppID)
                .FirstOrDefaultAsync();

            var hocPhiModels = await _aPI_Application_V1Context.HocPhiModels
                .Where(h => h.StudentId == studentId && h.AppID == appID).Include(x => x.HocPhiChiTietModels)
                .ToListAsync();
            var diemdanhInMonthYear = await _aPI_Application_V1Context.DiemDanhModels
                        .Where(s => s.StudentId == studentId)
                        .ToListAsync();

            var gia = await _aPI_Application_V1Context.GiaTiens.ToListAsync();
            if (studentId == null)
            {
                return new GetHocPhiModel2Response
                {
                    Success = false,
                    Error = "Error",
                    ErrorCode = "T05"
                };
            }

            decimal tongphioff = 0;
            if (gia != null)
            {
                foreach (var hocPhi in hocPhiModels)
                {
                    var totalOffDays = 0;
                    foreach (var sobuoi in diemdanhInMonthYear)
                    {
                        var monthYear = $"{sobuoi.CreateDate.Month}/{sobuoi.CreateDate.Year}";

                        if (monthYear == $"{hocPhi.Months}/{hocPhi.Years}" && sobuoi.DenLop != true)
                        {
                            totalOffDays++;
                        }

                    }
                    decimal totalPrice = 0;
                    foreach (var giatien in gia)
                    {
                        totalPrice += giatien.gia * totalOffDays;
                    }


                    hocPhi.Sobuoioff = totalOffDays;
                    tongphioff = totalPrice;
                    hocPhi.PhiOff = tongphioff;
                    hocPhi.ConLai = hocPhi.Total - hocPhi.PhiOff;
                   
                }
               
            }
           
            

            await _aPI_Application_V1Context.SaveChangesAsync();

            var hocPhiDTOs = hocPhiModels.Select(hocPhi => new HocPhiModel2
            {
                Id = hocPhi.Id,
                StudentId = hocPhi.StudentId,
                AppID = appID,
                ConLai = hocPhi.ConLai,
                Content = hocPhi.Content,
                PhiOff = hocPhi.PhiOff,
                UpdateDate = hocPhi.UpdateDate,
                Status = hocPhi.Status,
                Total = hocPhi.Total,
                CreateDate = hocPhi.CreateDate,
                Months = hocPhi.Months,
                Years = hocPhi.Years,
                IsCompleted = hocPhi.IsCompleted,
                Sobuoioff = hocPhi.Sobuoioff ?? 0,
                 HocPhiChiTietModels = hocPhi.HocPhiChiTietModels.Select(c => new HocPhiChiTietModel2
                {

                    Id = c.Id,
                    StudentId = c.StudentId,
                    AppID = c.AppID,
                    HocPhiId = c.HocPhiId,
                    CreateDate= c.CreateDate,
                    UpdateDate= c.UpdateDate,
                    Content= c.Content,
                    MaterHocPhiId = c.MaterHocPhiId,
                    Total = c.Total,
                    Quantity = c.Quantity,
                }).ToList()
            }).ToList();

            return new GetHocPhiModel2Response { Success = true, HocPhiModels = hocPhiDTOs };
        }



        public async Task<NewHocPhiModel2Response> newHocPhiModel2Response(string idClass, string idKhoaHoc, decimal tienhocphi, string month, int userId , decimal tienan , string years )
        {

            var appID = await _aPI_Application_V1Context.Users
              .Where(o => o.Id == userId)
              .Select(u => u.AppID)
              .FirstOrDefaultAsync();
            var masterHophi = await _aPI_Application_V1Context.MaterHocPhis.ToArrayAsync();
            var student = await _aPI_Application_V1Context.Students
              .Where(o => o.Class1 == idClass && o.Year1 == idKhoaHoc).ToArrayAsync();
            var khoa = await _aPI_Application_V1Context.KhoaHocModels.Where(k=>k.Id == int.Parse(idKhoaHoc)).FirstOrDefaultAsync();    
            if (student != null)
            {
                foreach (var item in student)
                {
                    var hocphi = await _aPI_Application_V1Context.HocPhiModels.Where(s => s.Months == month && s.Years == years && s.StudentId == item.Id)
                        .Include(x => x.HocPhiChiTietModels)
                        .ToArrayAsync();
                    if (hocphi.Length != 0)
                    {
                        foreach (var hocphi1 in hocphi)
                        {
                            hocphi1.AppID = appID;
                            hocphi1.Total = tienan + tienhocphi;
                            hocphi1.Years = "2024";
                            hocphi1.UpdateDate = DateTime.Now;
                            hocphi1.Content = "Học phí Tháng" + month;

                            foreach (var master in masterHophi)
                            {
                                if (master.Content == "Tiền học phí")
                                {
                                    var hocPhiChiTiet = hocphi1.HocPhiChiTietModels.FirstOrDefault(h => h.HocPhiId == hocphi1.Id);
                                    if (hocPhiChiTiet != null)
                                    {
                                        hocphi1.AppID = appID;
                                        hocPhiChiTiet.UpdateDate = DateTime.Now;
                                        hocPhiChiTiet.Total = tienhocphi;
                                    }
                                }

                                if (master.Content == "Tiền ăn")
                                {
                                    var hocPhiChiTiet = hocphi1.HocPhiChiTietModels.FirstOrDefault(h => h.HocPhiId == hocphi1.Id);
                                    if (hocPhiChiTiet != null)
                                    {
                                        hocphi1.AppID = appID;
                                        hocPhiChiTiet.UpdateDate = DateTime.Now;
                                        hocPhiChiTiet.Total = tienan;
                                    }
                                }
                            }
                        }
                        await _aPI_Application_V1Context.SaveChangesAsync();
                    }
                      

                      
                    else
                    {

                        var addhocphi = new HocPhiModel2
                        {
                            StudentId = item.Id,
                            Role = item.Role,
                            Content = "Học phí Tháng" + month,
                            UserId = userId,
                            Months = month,
                            Years = years,
                            CreateDate = DateTime.Now,
                            Total = tienhocphi + tienan,
                            IsCompleted = true,
                            AppID = appID,

                        };
                        foreach (var master in masterHophi)
                        {
                            if (master.Content == "Tiền học phí")
                                addhocphi.HocPhiChiTietModels.Add(new HocPhiChiTietModel2
                                {
                                    UserId = 1,
                                    StudentId = item.Id,
                                    HocPhiId = addhocphi.Id,
                                    CreateDate = DateTime.Now,
                                    Content = addhocphi.Content,
                                    AppID = appID,
                                    MaterHocPhiId = master.Id,
                                    Total = tienhocphi,
                                    Quantity = 1,
                                });

                            if (master.Content == "Tiền ăn")
                            {
                                addhocphi.HocPhiChiTietModels.Add(new HocPhiChiTietModel2
                                {
                                    UserId = 1,
                                    StudentId = item.Id,
                                    HocPhiId = addhocphi.Id,
                                    CreateDate = DateTime.Now,
                                    Content = "Tiền ăn",
                                    AppID = appID,
                                    MaterHocPhiId = master.Id,
                                    Total = tienan,
                                    Quantity = 1,

                                });
                            }
                        }
                        _aPI_Application_V1Context.AddRange(addhocphi);
                        await _aPI_Application_V1Context.SaveChangesAsync();
                    }


                }

                return new NewHocPhiModel2Response { Success = true };

            }
            else
            {
                return new NewHocPhiModel2Response
                {
                    Success = false,
                    Error = "Error",
                    ErrorCode = "T01"
                };

            }
        }






        public async Task<UpdateStatusHocPhiModelResponse> UpdateStatusHocPhi(int id, bool statusHocPhi)
        {
            var chiTietHocPhiById = await _aPI_Application_V1Context.HocPhiModels.FindAsync(id);
            if (chiTietHocPhiById == null)
            {
                return new UpdateStatusHocPhiModelResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }

            chiTietHocPhiById.Status = statusHocPhi;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateStatusHocPhiModelResponse
                {
                    Success = true,
                    HocPhiModel = chiTietHocPhiById
                };
            }
            return new UpdateStatusHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }


    }
}
