using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DiemDanhs;
using API_WebApplication.Interfaces.Reports;
using API_WebApplication.Services.DiemDanhs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            this._reportService = reportService;
        }
        [Authorize]
        [HttpGet("byday")]
        public async Task<IActionResult> GetListTotalDiemDanhTheoDay(int khoaHocId, int classId, int days, int months, int years)
        {
            var getReportsResponse = await _reportService.GetListTotalDiemDanhTheoDay(UserID_Protected, khoaHocId, classId, days, months, years);
            if (!getReportsResponse.Success)
            {
                return UnprocessableEntity(getReportsResponse);
            }
            return Ok(getReportsResponse);
        }

        [Authorize]
        [HttpGet("bymonth")]
        public async Task<IActionResult> GetListTotalDiemDanhTheoMonth(int khoaHocId, int classId, int months, int years)
        {
            var getReportsResponse = await _reportService.GetListTotalDiemDanhTheoMonth(UserID_Protected, khoaHocId, classId, months, years);
            if (!getReportsResponse.Success)
            {
                return UnprocessableEntity(getReportsResponse);
            }
            return Ok(getReportsResponse);
        }


        //Thắng làm phần BaoCaoDoanhThu
        //[Authorize] Tạm thời không có authorize cho dễ test
        [HttpGet("baocaodoanhthu")]
        public async Task<IActionResult> BaoCaoDoanhThu( int? khoaHocId, int? classId)
        {
            var getBaoCaoHocPhi = await _reportService.BaoCaoDoanhThu(UserID_Protected, khoaHocId, classId);
             if (!getBaoCaoHocPhi.Success)
            {
                return UnprocessableEntity(getBaoCaoHocPhi);
            }
            return Ok(getBaoCaoHocPhi);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> BaoCaoDoanhThuMonth()
        {
            var getBaoCaoHocPhi = await _reportService.BaoCaoDoanhThuMonth(UserID_Protected);
            if (!getBaoCaoHocPhi.Success)
            {
                return UnprocessableEntity(getBaoCaoHocPhi);
            }
            return Ok(getBaoCaoHocPhi);
        }
    }
}
