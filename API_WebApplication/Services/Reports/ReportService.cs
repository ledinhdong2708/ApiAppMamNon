using API_WebApplication.Interfaces.Reports;
using API_WebApplication.Models;
using API_WebApplication.Requests.BaoCaoDoanhThu;
using API_WebApplication.Requests.Reports;
using API_WebApplication.Responses.BaoCaoDoanhThu;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.Reports;
using API_WebApplication.Responses.SoBeNgoans;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_WebApplication.Services.Reports
{
    public class ReportService: IReportService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public ReportService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }
        public async Task<GetReportReponse> GetListTotalDiemDanhTheoDay(int userId, int khoaHocId, int classId,int days, int months, int years)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var reportDay = (
                await _aPI_Application_V1Context.DiemDanhModels
                .Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                .Where(_ => _.p.StudentId == _.c.Id
                && _.c.Year1 == khoaHocId.ToString()
                && _.c.Class1 == classId.ToString()
                && _.p.AppID == appID!.AppID
                && _.p.CreateDate.Day == days
                && _.p.CreateDate.Month == months
                && _.p.CreateDate.Year == years)
                .GroupBy(x => x.p.StudentId)
                .Select(pcu => new ReportDiemDanhRequest
                {
                    AppID = pcu.First().p.AppID,
                    StudentId = pcu.First().p.StudentId,
                    SoNgayHoc = pcu.Where(a => a.p.DenLop == true).Count(),
                    SoNgayNghi = pcu.Where(a => a.p.KhongPhep == true || a.p.CoPhep == true).Count(),
                    Student = _aPI_Application_V1Context.Students.Where(_ => _.Id == pcu.First().p.StudentId)!.FirstOrDefault()
                }).ToListAsync()
            );
#pragma warning restore CS8601 // Possible null reference assignment.

            return new GetReportReponse { Success = true, ReportDiemDanhRequests = reportDay.ToList() };
        }

        public async Task<GetReportReponse> GetListTotalDiemDanhTheoMonth(int userId, int khoaHocId, int classId, int months, int years)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var reportDay = (
                await _aPI_Application_V1Context.DiemDanhModels
                .Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                .Where(_ => _.p.StudentId == _.c.Id
                && _.c.Year1 == khoaHocId.ToString()
                && _.c.Class1 == classId.ToString()
                && _.p.AppID == appID!.AppID
                && _.p.CreateDate.Month == months
                && _.p.CreateDate.Year == years)
                .GroupBy(x => x.p.StudentId)
                .Select(pcu => new ReportDiemDanhRequest
                {
                    AppID = pcu.First().p.AppID,
                    StudentId = pcu.First().p.StudentId,
                    SoNgayHoc = pcu.Where(a => a.p.DenLop == true).Count(),
                    SoNgayNghi = pcu.Where(a => a.p.KhongPhep == true || a.p.CoPhep == true).Count(),
                    Student = _aPI_Application_V1Context.Students.Where(_ => _.Id == pcu.First().p.StudentId)!.FirstOrDefault()
                }).ToListAsync()
            );
#pragma warning restore CS8601 // Possible null reference assignment.

            return new GetReportReponse { Success = true, ReportDiemDanhRequests = reportDay.ToList() };
        }
        //Thắng làm phần BaoCaoDoanhThu
        public async Task<BaoCaoDoanhThuResponse> BaoCaoDoanhThu(int userId, int? khoaHocId, int? classId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            List<BaoCaoDoanhThuRequest> student;
            if (khoaHocId != 0 || classId != 0)
            {
                student = await _aPI_Application_V1Context.HocPhiModels
                    .Include(hocPhi => hocPhi.HocPhiChiTietModels)
                    .Join(_aPI_Application_V1Context.Students, hocPhi => hocPhi.StudentId, student => student.Id, (hocPhi, student) => new { hocPhi, student })
                    .Where(joinResult =>
                        joinResult.hocPhi.StudentId == joinResult.student.Id
                        && joinResult.hocPhi.AppID == appID!.AppID
                        && joinResult.student.Year1 == khoaHocId.ToString()
                        && joinResult.student.Class1 == classId.ToString()
                    )
                    .Select(o => new BaoCaoDoanhThuRequest
                    {
                        month = o.hocPhi.Months,
                        total = o.hocPhi.Total,
                    })
                    .OrderByDescending(o => o.month)
                    .ToListAsync();

            }
            else
            {
                student = await _aPI_Application_V1Context.HocPhiModels
                    .Include(hocPhi => hocPhi.HocPhiChiTietModels)
                    .Join(_aPI_Application_V1Context.Students, hocPhi => hocPhi.StudentId, student => student.Id, (hocPhi, student) => new { hocPhi, student })
                    .Where(joinResult =>
                       joinResult.hocPhi.AppID == appID!.AppID
                    )
                .Select(o => new BaoCaoDoanhThuRequest
                {
                    month = o.hocPhi.Months,
                    total = o.hocPhi.Total,
                })
                .OrderByDescending(o => o.month)
                .ToListAsync();
            }
                        return new BaoCaoDoanhThuResponse { Success = true, BaoCaoDoanhThu = student.ToList() };
        }

         public async Task<BaoCaoDoanhThuResponse> BaoCaoDoanhThuMonth(int userId)
    {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            List<BaoCaoDoanhThuRequest> student;
            if (appID != null)
            {
                student = await _aPI_Application_V1Context.HocPhiModels
                    .Where(joinResult =>
                        joinResult.AppID == appID!.AppID
                        && joinResult.CreateDate != null
                    )
                    .Select(o => new BaoCaoDoanhThuRequest
                    {
                        month = o.Months,
                        total = o.Total,
                    })
                    .ToListAsync();

                // Sắp xếp danh sách chỉ khi tất cả các giá trị trong trường month có thể chuyển đổi thành số nguyên
                if (student.All(item => int.TryParse(item.month, out _)))
                {
                    student = student.OrderBy(o => int.Parse(o.month)).ToList(); // Sắp xếp theo giá trị số của tháng
                    return new BaoCaoDoanhThuResponse { Success = true, BaoCaoDoanhThu = student };
                }
                else
                {
                    return new BaoCaoDoanhThuResponse { Success = false, Error = "Some values in 'month' field cannot be converted to integer." };
                }
            }
            else
            {
                return new BaoCaoDoanhThuResponse { Success = false, Error = "Error" };
            }


        }
    }


}
