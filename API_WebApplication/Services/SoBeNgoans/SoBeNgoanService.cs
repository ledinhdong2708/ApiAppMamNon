using API_WebApplication.Interfaces.SoBeNgoans;
using API_WebApplication.Models;
using API_WebApplication.Responses.SoBeNgoans;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API_WebApplication.Services.SoBeNgoans
{
    public class SoBeNgoanService : ISoBeNgoanService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public SoBeNgoanService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteSoBeNgoanResponse> DeleteSoBeNgoan(int sobengoanId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.Where(o => o.Id == sobengoanId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.FindAsync(sobengoanId);
            if (sobengoan == null)
            {
                return new DeleteSoBeNgoanResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (sobengoan.UserId != userId)
            {
                return new DeleteSoBeNgoanResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.SoBeNgoans.Remove(sobengoan);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteSoBeNgoanResponse
                {
                    Success = true,
                    SoBeNgoanId = sobengoan.Id
                };
            }
            return new DeleteSoBeNgoanResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<SoBeNgoanResponse> GetIDSoBeNgoan(int userId, int sobengoanId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            //var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.Include(o => o.Students).FirstOrDefaultAsync(x => x.Id == sobengoanId);
            var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.Where(x => x.Id == sobengoanId && x.AppID == appID!.AppID).Select(o => new SoBeNgoan
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                MonthSBN = o.MonthSBN,
                Tuan1 = o.Tuan1,
                Tuan2 = o.Tuan2,
                Tuan3 = o.Tuan3,
                Tuan4 = o.Tuan4,
                Tuan5 = o.Tuan5,
                NhanXet = o.NhanXet,
                ClassSBN = o.ClassSBN,
                YearSBN = o.YearSBN,
                idStudent = o.idStudent,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                CreateDate = o.CreateDate,
                UserId = o.UserId,
                Students = (ICollection<Student>)_aPI_Application_V1Context.Students.Where(_ => _.Id == o.idStudent)
            }).FirstOrDefaultAsync();
            //var sobengoan = await _aPI_Application_V1Context.SoBeNgoan.FindAsync(sobengoanId);
            if (sobengoan == null)
            {
                return new SoBeNgoanResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (sobengoan.UserId != userId)
            {
                return new SoBeNgoanResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (sobengoan.Id < 0)
            {
                return new SoBeNgoanResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new SoBeNgoanResponse
            {
                Success = true,
                SoBeNgoan = sobengoan
            };
        }

        public async Task<GetSoBeNgoanResponse> GetSoBeNgoansByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            //var sobengoan = await _aPI_Application_V1Context.SoBeNgoan.Where(o => o.UserId == userId).ToListAsync();
            var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.Where(o => o.UserId == userId && o.AppID == appID!.AppID).Select(o => new SoBeNgoan
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                MonthSBN = o.MonthSBN,
                Tuan1 = o.Tuan1,
                Tuan2 = o.Tuan2,
                Tuan3 = o.Tuan3,
                Tuan4 = o.Tuan4,
                Tuan5 = o.Tuan5,
                NhanXet = o.NhanXet,
                ClassSBN = o.ClassSBN,
                YearSBN = o.YearSBN,
                idStudent = o.idStudent,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                CreateDate = o.CreateDate,
                UserId = o.UserId,
                Students = (ICollection<Student>)_aPI_Application_V1Context.Students.Where(_ => _.Id == o.idStudent)
            }).OrderByDescending(o => o.CreateDate).ToListAsync();
            return new GetSoBeNgoanResponse { 
                Success = true, 
                SoBeNgoans = sobengoan.ToList() 
            };
        }

        public async Task<SaveSoBeNgoanResponse> SaveSoBeNgoan(SoBeNgoan sobengoan)
        {
            await _aPI_Application_V1Context.SoBeNgoans.AddAsync(sobengoan);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveSoBeNgoanResponse
                {
                    Success = true,
                    SoBeNgoan = sobengoan
                };
            }
            return new SaveSoBeNgoanResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateSoBeNgoanResponse> UpdateSoBeNgoan(int sobengoanId, SoBeNgoan sobengoan)
        {
            var sobengoanById = await _aPI_Application_V1Context.SoBeNgoans.FindAsync(sobengoanId);
            if (sobengoanById == null)
            {
                return new UpdateSoBeNgoanResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (sobengoan.UserId != sobengoanById.UserId)
            {
                return new UpdateSoBeNgoanResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }

            sobengoanById.MonthSBN = sobengoan.MonthSBN;
            sobengoanById.Tuan1 = sobengoan.Tuan1;
            sobengoanById.Tuan2 = sobengoan.Tuan2;
            sobengoanById.Tuan3 = sobengoan.Tuan3;
            sobengoanById.Tuan4 = sobengoan.Tuan4;
            sobengoanById.Tuan5 = sobengoan.Tuan5;
            sobengoanById.NhanXet = sobengoan.NhanXet;
            sobengoanById.ClassSBN = sobengoan.ClassSBN;
            sobengoanById.YearSBN = sobengoan.YearSBN;
            sobengoanById.idStudent = sobengoan.idStudent;
            sobengoanById.ChieuCao = sobengoan.ChieuCao;
            sobengoanById.CanNang = sobengoan.CanNang;
            sobengoanById.UpdateDate = sobengoan.UpdateDate;
            sobengoanById.IsCompleted = sobengoan.IsCompleted;
            sobengoanById.AppID = sobengoan.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateSoBeNgoanResponse
                {
                    Success = true,
                    SoBeNgoan = sobengoan
                };
            }
            return new UpdateSoBeNgoanResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetSoBeNgoanResponse> GetSoBeNgoanFillterByUserYearClass(int userId, string year, string classId, string month)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var soBeNgoan = await _aPI_Application_V1Context.SoBeNgoans.Where(o => o.UserId == userId
            && o.YearSBN.ToString() == year
            && o.ClassSBN == classId && o.MonthSBN.ToString() == month && o.AppID == appID!.AppID)
                .Select(o => new SoBeNgoan
                {
                    Id = o.Id,
                    IsCompleted = o.IsCompleted,
                    MonthSBN = o.MonthSBN,
                    Tuan1 = o.Tuan1,
                    Tuan2 = o.Tuan2,
                    Tuan3 = o.Tuan3,
                    Tuan4 = o.Tuan4,
                    Tuan5 = o.Tuan5,
                    NhanXet = o.NhanXet,
                    ClassSBN = o.ClassSBN,
                    YearSBN = o.YearSBN,
                    idStudent = o.idStudent,
                    CanNang = o.CanNang,
                    ChieuCao = o.ChieuCao,
                    CreateDate = o.CreateDate,
                    UserId = o.UserId,
                    Students = (ICollection<Student>)_aPI_Application_V1Context.Students.Where(_ => _.Id == o.idStudent).Select(o => new Student
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
                    })
                }).OrderByDescending(o => o.CreateDate)
                .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return new GetSoBeNgoanResponse { Success = true, SoBeNgoans = soBeNgoan.ToList() };
        }

        public async Task<SoBeNgoanResponse> PH_GetIDByStudent(int id, string month, string year, int userId)
        {
            //var sobengoan = await _aPI_Application_V1Context.SoBeNgoans.Include(o => o.Students).FirstOrDefaultAsync(x => x.Id == id);

            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var sobengoan1 = (
                from a in _aPI_Application_V1Context.SoBeNgoans
                join b in _aPI_Application_V1Context.Students on a.idStudent equals b.Id
                select new
                {
                    a.Id,
                    a.UserId,
                    a.Role,
                    a.MonthSBN,
                    a.Tuan1,
                    a.Tuan2,
                    a.Tuan3,
                    a.Tuan4,
                    a.Tuan5,
                    a.CreateDate,
                    a.UpdateDate,
                    a.IsCompleted,
                    a.NhanXet,
                    a.ChieuCao,
                    a.CanNang,
                    a.ClassSBN,
                    a.YearSBN,
                    a.idStudent,
                    Students = a.Students.Where(x=>x.Id == a.Id).FirstOrDefault(),
                    //student = a.Students.Select(x => new {
                    //    b.Id,
                    //    b.UserId,
                    //    b.Role,
                    //    b.NameStudent,
                    //    b.Year1,
                    //    b.Class1,
                    //    b.Year2,
                    //    b.Class2,
                    //    b.Year3,
                    //    b.Class3,
                    //    b.CreateDate,
                    //    b.UpdateDate,
                    //    b.IsCompleted,
                    //    b.img,
                    //    b.GV1,
                    //    b.GV2,
                    //    b.GV3,
                    //    b.CanNang,
                    //    b.ChieuCao,
                    //    b.SoBeNgoanId,
                    //    b.PhuHuynhId,
                    //    b.imagePatch,
                    //}),
                }) ;
            var studentId = await _aPI_Application_V1Context.Students.FindAsync(id);
            var sobengoan = await _aPI_Application_V1Context.SoBeNgoans
                .Join(_aPI_Application_V1Context.Students, p => p.idStudent, c => c.Id, (p, c) => new { p, c })
                .Where(_ => _.p.idStudent == id && _.p.ClassSBN == studentId.Class1
                && _.p.MonthSBN.ToString() == month && _.p.YearSBN.ToString() == year && _.p.AppID == appID!.AppID)
                .Select(pcu => new SoBeNgoan
                {
                    Id = pcu.p.Id,
                    UserId = pcu.p.UserId,
                    Role = pcu.p.Role,
                    MonthSBN = pcu.p.MonthSBN,
                    Tuan1 = pcu.p.Tuan1,
                    Tuan2 = pcu.p.Tuan2,
                    Tuan3 = pcu.p.Tuan3,
                    Tuan4 = pcu.p.Tuan4,
                    Tuan5 = pcu.p.Tuan5,
                    CreateDate = pcu.p.CreateDate,
                    UpdateDate = pcu.p.UpdateDate,
                    IsCompleted = pcu.p.IsCompleted,
                    NhanXet = pcu.p.NhanXet,
                    ChieuCao = pcu.p.ChieuCao,
                    CanNang = pcu.p.CanNang,
                    ClassSBN = pcu.p.ClassSBN,
                    YearSBN = pcu.p.YearSBN,
                    idStudent = pcu.p.idStudent,
                    Students = (ICollection<Student>)_aPI_Application_V1Context.Students.Where(_ => _.Id == pcu.p.idStudent)
                    //_aPI_Application_V1Context.Students.Where(_ => _.Id == pcu.p.idStudent).Select(o => new Student
                    //{
                    //    Id = o.Id,
                    //    IsCompleted = o.IsCompleted,
                    //    NameStudent = o.NameStudent,
                    //    Year1 = o.Year1,
                    //    Year2 = o.Year2,
                    //    Year3 = o.Year3,
                    //    Class1 = o.Class1,
                    //    Class2 = o.Class2,
                    //    Class3 = o.Class3,
                    //    GV1 = o.GV1,
                    //    GV2 = o.GV2,
                    //    GV3 = o.GV3,
                    //    CanNang = o.CanNang,
                    //    ChieuCao = o.ChieuCao,
                    //    CreateDate = o.CreateDate,
                    //    img = o.img,
                    //    imagePatch = o.imagePatch
                    //}).FirstOrDefaultAsync()
                }).FirstOrDefaultAsync();
            if (sobengoan == null)
            {
                return new SoBeNgoanResponse
                {
                    Success = false,
                    Error = "So be ngoan not found",
                    ErrorCode = "T01"
                };
            }
            if (sobengoan == null)
            {
                return new SoBeNgoanResponse
                {
                    Success = true,
                    SoBeNgoan = {}
                };
            }
            //if (sobengoan.Id < 0)
            //{
            //    return new SoBeNgoanResponse
            //    {
            //        Success = true,
            //        SoBeNgoan = {}
            //    };
            //}
            return new SoBeNgoanResponse
            {
                Success = true,
                SoBeNgoan = (SoBeNgoan)sobengoan
            };
        }


    }
}
