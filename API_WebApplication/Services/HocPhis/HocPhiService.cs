using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.Interfaces.HocPhis;
using API_WebApplication.Models;
using API_WebApplication.Responses.HocPhis;
using API_WebApplication.Responses.SoBeNgoans;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieu;
using API_WebApplication.Responses.TinTuc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq;

namespace API_WebApplication.Services.HocPhis
{
    public class HocPhiService : IHocPhiService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly IMapper _mapper;

        public HocPhiService(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            this._mapper = mapper;
        }

        public async Task<HocPhiResponse> GetIDHocPhi(int userId, int hocphiId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = await _aPI_Application_V1Context.HocPhis.Include(_ => _.ChiTietHocPhis).ThenInclude(_ => _.ChiTietHocPhiTheoMonths).Where(_ => _.Id == hocphiId && _.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (hocphi == null)
            {
                return new HocPhiResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphi.UserId != userId)
            {
                return new HocPhiResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (hocphi.Id < 0)
            {
                return new HocPhiResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HocPhiResponse
            {
                Success = true,
                Data = hocphi
            };
        }

        public async Task<DeleteHocPhiResponse> DeleteHocPhi(int hocphiId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = await _aPI_Application_V1Context.HocPhis.Where(o => o.Id == hocphiId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var hocphi = await _aPI_Application_V1Context.HocPhis.FindAsync(hocphiId);
            if (hocphi == null)
            {
                return new DeleteHocPhiResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphi.UserId != userId)
            {
                return new DeleteHocPhiResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.HocPhis.Remove(hocphi);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteHocPhiResponse
                {
                    Success = true,
                    HocPhiId = hocphi.Id
                };
            }
            return new DeleteHocPhiResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetHocPhiResponse> GetHocPhisByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = await _aPI_Application_V1Context.HocPhis.Where(_ => _.UserId == userId && _.AppID == appID!.AppID).Include(_ => _.ChiTietHocPhis).ThenInclude(_=>_.ChiTietHocPhiTheoMonths).OrderByDescending(o => o.CreateDate).ToListAsync();
            if (hocphi.Count == 0)
            {
                return new GetHocPhiResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04",
                    Data = hocphi.ToList()
                };
            }
            return new GetHocPhiResponse { Success = true, HocPhis = hocphi };
        }

        public async Task<SaveHocPhiResponse> SaveHocPhi(HocPhiDTO _hocphidto)
        {
            var newTKB = _mapper.Map<HocPhi>(_hocphidto);
            await _aPI_Application_V1Context.AddAsync(newTKB);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            //return Created($"/ThoiKhoaBieu/{newTKB.Id}", newTKB);

            //await _aPI_Application_V1Context.Student.AddAsync(student);
            //var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveHocPhiResponse
                {
                    Success = true,
                    Data = newTKB
                };
            }
            return new SaveHocPhiResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateHocPhiResponse> UpdateHocPhi(int hocphiId, HocPhiDTO _hocphidto)
        {
            var updateOTKB = _mapper.Map<HocPhi>(_hocphidto);
            _aPI_Application_V1Context.Update(updateOTKB);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiResponse
                {
                    Success = true,
                    HocPhi = _hocphidto
                };
            }
            return new UpdateHocPhiResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateHocPhi_DetailResponse> UpdateHocPhi_Detail(int hocphiId, HocPhi _hocphidto)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == _hocphidto.UserId).FirstOrDefaultAsync();
            var hocphiById = await _aPI_Application_V1Context.HocPhis.FindAsync(hocphiId);
            //var hocphiById = await _aPI_Application_V1Context.HocPhis.FindAsync(hocphiId);

            if (hocphiById == null)
            {
                return new UpdateHocPhi_DetailResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphiById.UserId != hocphiById.UserId)
            {
                return new UpdateHocPhi_DetailResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }
            hocphiById.Content = _hocphidto.Content;
            hocphiById.UserId = _hocphidto.UserId;
            hocphiById.TotalMax = _hocphidto.TotalMax;
            hocphiById.UpdateDate = _hocphidto.UpdateDate;
            hocphiById.AppID = _hocphidto.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhi_DetailResponse
                {
                    Success = true,
                    HocPhi = _hocphidto
                };
            }
            return new UpdateHocPhi_DetailResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<GetHocPhiChiTietTheoMonthResponse> GetHocPhiChiTietTheoMonthByIDHocPhiChiTiet(int hocphiId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = await _aPI_Application_V1Context.ChiTietHocPhiTheoMonths.Where(_ => _.HocPhiChiTietId == hocphiId).OrderByDescending(o => o.CreateDate).ToListAsync();
            if (hocphi.Count == 0)
            {
                return new GetHocPhiChiTietTheoMonthResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04",
                    Data = hocphi.ToList()
                };
            }
            return new GetHocPhiChiTietTheoMonthResponse { Success = true, ChiTietHocPhiTheoMonths = hocphi };
        }

        public async Task<GetHocPhiResponse> GetSoBeNgoanFillterByUserKhoaHocClassStudent(int userId, string year, string classId)
        {
            //var hocPhi = await _aPI_Application_V1Context.Students.Where(_ => _.Year1 == year).Join(HocPhi,p => p.Id, pc => pc.studentId,(p,pc)).HocPhis.Where(o => o.UserId == userId
            //&& o.CreateDate.Value.Year.ToString() == year
            //&& o.ClassSBN == classId).ToListAsync();
            //var hocPhi = await _aPI_Application_V1Context.Students.Where(_ => _.Year1 == year && _.Class1 == classId && _.Id.ToString() == studentId)
            //    .Join(_aPI_Application_V1Context.HocPhis,p => p.Id, c => c.StudentId, (p,c) => new {p,c})
            //    .Select(pcu => new
            //    {
            //        pcu.c.Id,
            //        pcu.c.Content,
            //        pcu.c.StudentId,
            //        pcu.c.UserId,
            //        pcu.c.CreateDate,
            //        pcu.c.UpdateDate,
            //        pcu.c.TotalMax,
            //        pcu.c.TotalMin,
            //        pcu.c.ChiTietHocPhis,
            //    }).ToListAsync();
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocPhi = await _aPI_Application_V1Context.HocPhis
                .Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                .Where(_ => _.c.Year1 == year && _.c.Class1 == classId && _.p.AppID == appID!.AppID)
                .Select(pcu => new HocPhi
                {
                   Id = pcu.p.Id,
                    Content= pcu.p.Content,
                    StudentId =pcu.p.StudentId,
                    UserId = pcu.p.UserId,
                    CreateDate = pcu.p.CreateDate,
                    UpdateDate = pcu.p.UpdateDate,
                    TotalMax = pcu.p.TotalMax,
                    TotalMin = pcu.p.TotalMin,
                    NameStudent = _aPI_Application_V1Context.Students.Where(x => x.Id == pcu.p.StudentId).FirstOrDefault().NameStudent,
                    ChiTietHocPhis = pcu.p.ChiTietHocPhis,
                }).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetHocPhiResponse { Success = true, HocPhis = hocPhi.ToList() };
        }
        public async Task<HocPhiResponse> PH_GetIDByHocPhiStudent(int id, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var hocphi = new HocPhi();
            var studentId = await _aPI_Application_V1Context.Students.Where(o => o.Id == id && o.IsCompleted == false).FirstOrDefaultAsync();
            if(studentId != null)
            {
                hocphi = await _aPI_Application_V1Context.HocPhis.Where(_ => _.StudentId == studentId.Id && _.AppID == appID!.AppID).Include(_ => _.ChiTietHocPhis).ThenInclude(_ => _.ChiTietHocPhiTheoMonths).FirstOrDefaultAsync();

            }
            if (hocphi == null)
            {
                return new HocPhiResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (hocphi.Id < 0)
            {
                return new HocPhiResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HocPhiResponse
            {
                Success = true,
                Data = hocphi
            };
        }
        public async Task<UpdateChiTietHocPhi> UpdateCompleteChiTietHocPhi(int id, bool statusHocPhi)
        {
            var chiTietHocPhiById = await _aPI_Application_V1Context.ChiTietHocPhis.FindAsync(id);
            if (chiTietHocPhiById == null)
            {
                return new UpdateChiTietHocPhi
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }

            chiTietHocPhiById.IsCompleted = statusHocPhi;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateChiTietHocPhi
                {
                    Success = true,
                    ChiTietHocPhi = chiTietHocPhiById
                };
            }
            return new UpdateChiTietHocPhi
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

    }
}
