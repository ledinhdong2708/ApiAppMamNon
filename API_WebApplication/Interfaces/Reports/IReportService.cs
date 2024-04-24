using API_WebApplication.Responses.BaoCaoDoanhThu;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.Reports;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Interfaces.Reports
{
    public interface IReportService
    {
        Task<GetReportReponse> GetListTotalDiemDanhTheoDay(int userId, int khoaHocId, int classId, int days, int months, int years);
        Task<GetReportReponse> GetListTotalDiemDanhTheoMonth(int userId, int khoaHocId, int classId, int months, int years);
        //Thắng làm phần BaoCaoDoanhThu
        Task<BaoCaoDoanhThuResponse> BaoCaoDoanhThu(int userId, int? khoaHocId, int? classId);
        Task<BaoCaoDoanhThuResponse> BaoCaoDoanhThuMonth(int userId);
    }
}
