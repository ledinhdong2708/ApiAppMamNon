using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.SoBeNgoans;
using API_WebApplication.Interfaces.Students;
using API_WebApplication.Models;
using API_WebApplication.Requests.SoBeNgoan;
using API_WebApplication.Responses.SoBeNgoans;
using API_WebApplication.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.SoBeNgoans
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoBeNgoanController : BaseApiController
    {
        private readonly ISoBeNgoanService _soBeNgoanService;
        private readonly IStudentService _studentService;
        public SoBeNgoanController(ISoBeNgoanService soBeNgoanService, IStudentService studentService)
        {
            _soBeNgoanService = soBeNgoanService;
            _studentService = studentService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getSoBeNgoansResponse = await _soBeNgoanService.GetSoBeNgoansByUser(UserID_Protected);
            if (!getSoBeNgoansResponse.Success)
            {
                return UnprocessableEntity(getSoBeNgoansResponse);
            }

            var tasksResponse = getSoBeNgoansResponse.SoBeNgoans.ConvertAll(o => new SoBeNgoan
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
                Students = o.Students,
                AppID = o.AppID,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byyearclass")]
        public async Task<IActionResult> GetAllFillterByUserYearClass(string year, string classId, string month)
        {
            var getSoBeNgoansResponse = await _soBeNgoanService.GetSoBeNgoanFillterByUserYearClass(UserID_Protected, year, classId, month);
            if (!getSoBeNgoansResponse.Success)
            {
                return UnprocessableEntity(getSoBeNgoansResponse);
            }

            var tasksResponse = getSoBeNgoansResponse.SoBeNgoans.ConvertAll(o => new SoBeNgoan
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
                Students = o.Students,
                AppID = o.AppID,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int SoBeNgoanID)
        {
            var getSoBeNgoanResponse = await _soBeNgoanService.GetIDSoBeNgoan(UserID, SoBeNgoanID);
            if (!getSoBeNgoanResponse.Success)
            {
                return UnprocessableEntity(getSoBeNgoanResponse);
            }

            var tasksResponse = new SoBeNgoan
            {
                Id = getSoBeNgoanResponse.SoBeNgoan.Id,
                IsCompleted = getSoBeNgoanResponse.SoBeNgoan.IsCompleted,
                MonthSBN = getSoBeNgoanResponse.SoBeNgoan.MonthSBN,
                Tuan1 = getSoBeNgoanResponse.SoBeNgoan.Tuan1,
                Tuan2 = getSoBeNgoanResponse.SoBeNgoan.Tuan2,
                Tuan3 = getSoBeNgoanResponse.SoBeNgoan.Tuan3,
                Tuan4 = getSoBeNgoanResponse.SoBeNgoan.Tuan4,
                Tuan5 = getSoBeNgoanResponse.SoBeNgoan.Tuan5,
                NhanXet = getSoBeNgoanResponse.SoBeNgoan.NhanXet,
                ClassSBN = getSoBeNgoanResponse.SoBeNgoan.ClassSBN,
                YearSBN = getSoBeNgoanResponse.SoBeNgoan.YearSBN,
                idStudent = getSoBeNgoanResponse.SoBeNgoan.idStudent,
                CanNang = getSoBeNgoanResponse.SoBeNgoan.CanNang,
                ChieuCao = getSoBeNgoanResponse.SoBeNgoan.ChieuCao,
                CreateDate = getSoBeNgoanResponse.SoBeNgoan.CreateDate,
                Students = getSoBeNgoanResponse.SoBeNgoan.Students,
                AppID = getSoBeNgoanResponse.SoBeNgoan.AppID
            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(SoBeNgoanRequest soBeNgoanRequest)
        {
            DateTime dateTime = DateTime.Now;
            var sobengoan = new SoBeNgoan
            {
                MonthSBN = soBeNgoanRequest.MonthSBN,
                Tuan1 = soBeNgoanRequest.Tuan1,
                Tuan2 = soBeNgoanRequest.Tuan2,
                Tuan3 = soBeNgoanRequest.Tuan3,
                Tuan4 = soBeNgoanRequest.Tuan4,
                Tuan5 = soBeNgoanRequest.Tuan5,
                NhanXet = soBeNgoanRequest.NhanXet,
                ClassSBN = soBeNgoanRequest.ClassSBN,
                YearSBN = soBeNgoanRequest.YearSBN,
                idStudent = soBeNgoanRequest.idStudent,
                CanNang = soBeNgoanRequest.CanNang,
                ChieuCao = soBeNgoanRequest.ChieuCao,
                CreateDate = dateTime,
                IsCompleted = soBeNgoanRequest.IsCompleted,
                UserId = UserID_Protected,
                Role = 0,
                AppID = soBeNgoanRequest.AppID
            };
            var saveSoBeNgoanResponse = await _soBeNgoanService.SaveSoBeNgoan(sobengoan);
            if (!saveSoBeNgoanResponse.Success)
            {
                return UnprocessableEntity(saveSoBeNgoanResponse);
            }
            var taskResponse = new SoBeNgoan
            {
                Id = saveSoBeNgoanResponse.SoBeNgoan.Id,
                IsCompleted = saveSoBeNgoanResponse.SoBeNgoan.IsCompleted,
                MonthSBN = saveSoBeNgoanResponse.SoBeNgoan.MonthSBN,
                Tuan1 = saveSoBeNgoanResponse.SoBeNgoan.Tuan1,
                Tuan2 = saveSoBeNgoanResponse.SoBeNgoan.Tuan2,
                Tuan3 = saveSoBeNgoanResponse.SoBeNgoan.Tuan3,
                Tuan4 = saveSoBeNgoanResponse.SoBeNgoan.Tuan4,
                Tuan5 = saveSoBeNgoanResponse.SoBeNgoan.Tuan5,
                NhanXet = saveSoBeNgoanResponse.SoBeNgoan.NhanXet,
                ClassSBN = saveSoBeNgoanResponse.SoBeNgoan.ClassSBN,
                YearSBN = saveSoBeNgoanResponse.SoBeNgoan.YearSBN,
                idStudent = saveSoBeNgoanResponse.SoBeNgoan.idStudent,
                CanNang = saveSoBeNgoanResponse.SoBeNgoan.CanNang,
                ChieuCao = saveSoBeNgoanResponse.SoBeNgoan.ChieuCao,
                CreateDate = saveSoBeNgoanResponse.SoBeNgoan.CreateDate,
                Students = saveSoBeNgoanResponse.SoBeNgoan.Students,
                AppID = saveSoBeNgoanResponse.SoBeNgoan.AppID
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteSoBeNgoanResponse = await _soBeNgoanService.DeleteSoBeNgoan(id, UserID_Protected);
            if (!deleteSoBeNgoanResponse.Success)
            {
                return UnprocessableEntity(deleteSoBeNgoanResponse);
            }
            return Ok(deleteSoBeNgoanResponse.SoBeNgoanId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SoBeNgoanRequest spBeNgoanUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var sobengoan = new SoBeNgoan
            {
                MonthSBN = spBeNgoanUpdateRequest.MonthSBN,
                Tuan1 = spBeNgoanUpdateRequest.Tuan1,
                Tuan2 = spBeNgoanUpdateRequest.Tuan2,
                Tuan3 = spBeNgoanUpdateRequest.Tuan3,
                Tuan4 = spBeNgoanUpdateRequest.Tuan4,
                Tuan5 = spBeNgoanUpdateRequest.Tuan5,
                NhanXet = spBeNgoanUpdateRequest.NhanXet,
                ClassSBN = spBeNgoanUpdateRequest.ClassSBN,
                YearSBN = spBeNgoanUpdateRequest.YearSBN,
                idStudent = spBeNgoanUpdateRequest.idStudent,
                CanNang = spBeNgoanUpdateRequest.CanNang,
                ChieuCao = spBeNgoanUpdateRequest.ChieuCao,
                UpdateDate = dateTime,
                IsCompleted = spBeNgoanUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = spBeNgoanUpdateRequest.AppID
            };
            var saveSoBeNgoanResponse = await _soBeNgoanService.UpdateSoBeNgoan(id, sobengoan);
            if (!saveSoBeNgoanResponse.Success)
            {
                return UnprocessableEntity(saveSoBeNgoanResponse);
            }
            var taskResponse = new SoBeNgoan
            {
                IsCompleted = saveSoBeNgoanResponse.SoBeNgoan.IsCompleted,
                MonthSBN = saveSoBeNgoanResponse.SoBeNgoan.MonthSBN,
                Tuan1 = saveSoBeNgoanResponse.SoBeNgoan.Tuan1,
                Tuan2 = saveSoBeNgoanResponse.SoBeNgoan.Tuan2,
                Tuan3 = saveSoBeNgoanResponse.SoBeNgoan.Tuan3,
                Tuan4 = saveSoBeNgoanResponse.SoBeNgoan.Tuan4,
                Tuan5 = saveSoBeNgoanResponse.SoBeNgoan.Tuan5,
                NhanXet = saveSoBeNgoanResponse.SoBeNgoan.NhanXet,
                ClassSBN = saveSoBeNgoanResponse.SoBeNgoan.ClassSBN,
                YearSBN = saveSoBeNgoanResponse.SoBeNgoan.YearSBN,
                idStudent = saveSoBeNgoanResponse.SoBeNgoan.idStudent,
                CanNang = saveSoBeNgoanResponse.SoBeNgoan.CanNang,
                ChieuCao = saveSoBeNgoanResponse.SoBeNgoan.ChieuCao,
                UpdateDate = dateTime,
                Students = saveSoBeNgoanResponse.SoBeNgoan.Students,
                AppID = saveSoBeNgoanResponse.SoBeNgoan.AppID
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("ph/byId")]
        public async Task<IActionResult> PH_GetByStudent(int StudentID, string month, string year)
        {
            var getSoBeNgoanResponse = await _soBeNgoanService.PH_GetIDByStudent(StudentID, month, year, UserID_Protected);
            if (!getSoBeNgoanResponse.Success)
            {
                return UnprocessableEntity(getSoBeNgoanResponse);
            }

            var tasksResponse = new SoBeNgoan
            {
                Id = getSoBeNgoanResponse.SoBeNgoan.Id,
                IsCompleted = getSoBeNgoanResponse.SoBeNgoan.IsCompleted,
                MonthSBN = getSoBeNgoanResponse.SoBeNgoan.MonthSBN,
                Tuan1 = getSoBeNgoanResponse.SoBeNgoan.Tuan1,
                Tuan2 = getSoBeNgoanResponse.SoBeNgoan.Tuan2,
                Tuan3 = getSoBeNgoanResponse.SoBeNgoan.Tuan3,
                Tuan4 = getSoBeNgoanResponse.SoBeNgoan.Tuan4,
                Tuan5 = getSoBeNgoanResponse.SoBeNgoan.Tuan5,
                NhanXet = getSoBeNgoanResponse.SoBeNgoan.NhanXet,
                ClassSBN = getSoBeNgoanResponse.SoBeNgoan.ClassSBN,
                YearSBN = getSoBeNgoanResponse.SoBeNgoan.YearSBN,
                idStudent = getSoBeNgoanResponse.SoBeNgoan.idStudent,
                CanNang = getSoBeNgoanResponse.SoBeNgoan.CanNang,
                ChieuCao = getSoBeNgoanResponse.SoBeNgoan.ChieuCao,
                CreateDate = getSoBeNgoanResponse.SoBeNgoan.CreateDate,
                Students = getSoBeNgoanResponse.SoBeNgoan.Students,
                AppID = getSoBeNgoanResponse.SoBeNgoan.AppID
            };
            return Ok(tasksResponse);
        }
    }
}
