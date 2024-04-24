using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Interfaces.XinNghiPheps;
using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.HocPhis;
using API_WebApplication.Responses.XinNghiPhep;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace API_WebApplication.Services.XinNghiPheps
{
    public class XinNghiPhepService : IXinNghiPhepService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly IMapper _mapper;
        public XinNghiPhepService(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            this._mapper = mapper;
        }

        public async Task<DeleteHocPhiModelResponse> DeleteXinNghiPhep(int XinNghiPhepId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels.Where(o => o.Id == XinNghiPhepId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels.FindAsync(XinNghiPhepId);
            if (xinNghiPhep == null)
            {
                return new DeleteHocPhiModelResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (xinNghiPhep.UserId != userId)
            {
                return new DeleteHocPhiModelResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.XinNghiPhepModels.Remove(xinNghiPhep);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteHocPhiModelResponse
                {
                    Success = true,
                    XinNghiPhepId = xinNghiPhep.Id
                };
            }
            return new DeleteHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<HocPhiModelResponse> GetIDXinNghiPhep(int userId, int XinNghiPhepId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels.Include(_ => _.ChiTietXinNghiPheps).Where(_ => _.Id == XinNghiPhepId && _.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (xinNghiPhep == null)
            {
                return new HocPhiModelResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (xinNghiPhep.UserId != userId)
            {
                return new HocPhiModelResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (xinNghiPhep.Id < 0)
            {
                return new HocPhiModelResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HocPhiModelResponse
            {
                Success = true,
                Data = xinNghiPhep
            };
        }

        public async Task<Responses.XinNghiPhep.GetHocPhiModelResponse> GetXinNghiPhepsByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels.Where(_ => _.UserId == userId && _.AppID == appID!.AppID).Include(_ => _.ChiTietXinNghiPheps).OrderByDescending(o => o.CreateDate).ToListAsync();
            if (xinNghiPhep.Count == 0)
            {
                return new Responses.XinNghiPhep.GetHocPhiModelResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04",
                    Data = xinNghiPhep.ToList()
                };
            }
            return new Responses.XinNghiPhep.GetHocPhiModelResponse { Success = true, XinNghiPheps = xinNghiPhep };
        }

        public async Task<SaveHocPhiModelResponse> SaveXinNghiPhep(XinNghiPhepDTO _XinNghiPhepDTO)
        {
            var newXinNghiPhep = _mapper.Map<XinNghiPhepModel>(_XinNghiPhepDTO);
            await _aPI_Application_V1Context.AddAsync(newXinNghiPhep);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveHocPhiModelResponse
                {
                    Success = true,
                    Data = newXinNghiPhep
                };
            }
            return new SaveHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<SaveHocPhiModelResponse> PH_SaveXinNghiPhep(XinNghiPhepDTO _XinNghiPhepDTO)
        {
            var newXinNghiPhep = _mapper.Map<XinNghiPhepModel>(_XinNghiPhepDTO);
            await _aPI_Application_V1Context.AddAsync(newXinNghiPhep);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveHocPhiModelResponse
                {
                    Success = true,
                    Data = newXinNghiPhep
                };
            }
            return new SaveHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHocPhiModelResponse> UpdateXinNghiPhep(int XinNghiPhepId, XinNghiPhepDTO _XinNghiPhepDTO)
        {
            var updateXinNghiPhep = _mapper.Map<XinNghiPhepModel>(_XinNghiPhepDTO);
            _aPI_Application_V1Context.Update(updateXinNghiPhep);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModelResponse
                {
                    Success = true,
                    XinNghiPhep = _XinNghiPhepDTO
                };
            }
            return new UpdateHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateHocPhiModelResponse> PH_UpdateXinNghiPhep(int XinNghiPhepId, XinNghiPhepDTO _XinNghiPhepDTO)
        {
            var updateXinNghiPhep = _mapper.Map<XinNghiPhepModel>(_XinNghiPhepDTO);
            _aPI_Application_V1Context.Update(updateXinNghiPhep);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModelResponse
                {
                    Success = true,
                    XinNghiPhep = _XinNghiPhepDTO
                };
            }
            return new UpdateHocPhiModelResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHocPhiModel_DetailResponse> UpdateXinNghiPhep_Detail(int XinNghiPhepId, ChiTietXinNghiPhep _XinNghiPhep)
        {
            var xinNghiPhepById = await _aPI_Application_V1Context.ChiTietXinNghiPheps.FindAsync(XinNghiPhepId);

            if (xinNghiPhepById == null)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (xinNghiPhepById.UserId != xinNghiPhepById.UserId)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }
            xinNghiPhepById.StudentId = _XinNghiPhep.StudentId;
            xinNghiPhepById.Content = _XinNghiPhep.Content;
            xinNghiPhepById.FromDate = _XinNghiPhep.FromDate;
            xinNghiPhepById.ToDate = _XinNghiPhep.ToDate;
            xinNghiPhepById.AppID = _XinNghiPhep.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = true,
                    XinNghiPhep = _XinNghiPhep
                };
            }
            return new UpdateHocPhiModel_DetailResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateHocPhiModel_DetailResponse> PH_UpdateXinNghiPhep_Detail(int XinNghiPhepId, ChiTietXinNghiPhep _XinNghiPhep)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == _XinNghiPhep.UserId).FirstOrDefaultAsync();
            var xinNghiPhepById = await _aPI_Application_V1Context.ChiTietXinNghiPheps.Where(o => o.Id == XinNghiPhepId && o.AppID == _XinNghiPhep.AppID).FirstOrDefaultAsync();
            //var xinNghiPhepById = await _aPI_Application_V1Context.ChiTietXinNghiPheps.FindAsync(XinNghiPhepId);

            if (xinNghiPhepById == null)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (xinNghiPhepById.UserId != xinNghiPhepById.UserId)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }
            xinNghiPhepById.StudentId = _XinNghiPhep.StudentId;
            xinNghiPhepById.Content = _XinNghiPhep.Content;
            xinNghiPhepById.FromDate = _XinNghiPhep.FromDate;
            xinNghiPhepById.ToDate = _XinNghiPhep.ToDate;
            xinNghiPhepById.AppID = _XinNghiPhep.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateHocPhiModel_DetailResponse
                {
                    Success = true,
                    XinNghiPhep = _XinNghiPhep
                };
            }
            return new UpdateHocPhiModel_DetailResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }

        public async Task<GetHocPhiModelResponse2> GetIDXinNghiPhepByDate(int userId, int day, int month, int year)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _xinnghiphep = await _aPI_Application_V1Context.XinNghiPhepModels.Include(_ => _.ChiTietXinNghiPheps)
                .Where(o =>
               o.ChiTietXinNghiPheps.Any(z => z.FromDate.Day <= day && z.FromDate.Month <=month && z.FromDate.Year <= year  && z.ToDate.Day >= day && z.ToDate.Month >= month && z.ToDate.Year >= year)
                //(o.CreateDate.Value.Day == day && o.CreateDate.Value.Month == month && o.CreateDate.Value.Year == year)
                // (o.CreateDate.Value.Year == year)
                &&
                o.AppID == appID!.AppID)
                .Select(o => new XinNghiphepModel2
                {
                    Content=o.Content,
                    CreateDate=o.CreateDate,
                    Id=o.Id,
                    IsCompleted=o.IsCompleted,
                    Role=o.Role,
                    StudentId=o.StudentId,
                    UpdateDate=o.UpdateDate,
                    UserId=o.UserId,
                    ChiTietXinNghiPheps = o.ChiTietXinNghiPheps,
                    //Student = _aPI_Application_V1Context.Students.Where(a => a.Id == o.StudentId).FirstOrDefault(),
                    Student = _aPI_Application_V1Context.Students.Where(_ => _.Id == o.StudentId).Select(o => new Student
                    {
                        Id = o.Id,
                        IsCompleted = o.IsCompleted,
                        NameStudent = o.NameStudent,
                        Year1 = _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault().FromYear.ToString()
                        + " - " + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault().ToYear.ToString(),
                        Year2 = o.Year2,
                        Year3 = o.Year3,
                        Class1 = _aPI_Application_V1Context.ClassModels.Where(_ => _.Id.ToString() == o.Class1).FirstOrDefault().NameClass.ToString(),
                        Class2 = o.Class2,
                        Class3 = o.Class3,
                        GV1 = o.GV1,
                        GV2 = o.GV2,
                        GV3 = o.GV3,
                        CanNang = o.CanNang,
                        ChieuCao = o.ChieuCao,
                        CreateDate = o.CreateDate,
                        img = o.img,
                        imagePatch = o.imagePatch
                    }).FirstOrDefault()
                })
                //.Where(a => a.ChiTietXinNghiPheps.Where(z => z.FromDate.Day == day && z.FromDate.Month == month && z.FromDate.Year == year))
                .OrderByDescending(o => o.CreateDate).ToListAsync();
            //var _xinnghiphep = await _aPI_Application_V1Context.XinNghiPheps
            //    .Include(_ => _.ChiTietXinNghiPheps)
            //    .Where(_ => _.ChiTietXinNghiPheps.Select(x => x.FromDate.Day.ToString() == day.ToString()).ToList()
            //    )
            //    .ToList();
            if (_xinnghiphep == null)
            {
                return new GetHocPhiModelResponse2
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            
            return new GetHocPhiModelResponse2
            {
                Success = true,
                XinNghiPheps = _xinnghiphep
            };
        }
        public async Task<HocPhiModelResponse> PH_GetIDXinNghiPhepByStudent(int id, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var studentId = await _aPI_Application_V1Context.Students.Where(o => o.Id == id && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            var xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels
                    .Where(_ => _.StudentId == studentId.Id)
                    .Include(_ => _.ChiTietXinNghiPheps).FirstOrDefaultAsync();

            //if (studentId != null)
            //{
            //    xinNghiPhep = await _aPI_Application_V1Context.XinNghiPhepModels
            //        .Where(_ => _.StudentId == studentId.Id)
            //        .Include(_ => _.ChiTietXinNghiPheps).FirstOrDefaultAsync();

            //}
            if (xinNghiPhep == null)
            {
                return new HocPhiModelResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (xinNghiPhep.Id < 0)
            {
                return new HocPhiModelResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new HocPhiModelResponse
            {
                Success = true,
                Data = xinNghiPhep
            };
        }
    }
}
