using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DiemDanhs;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Models;
using API_WebApplication.Requests.DiemDanh;
using API_WebApplication.Requests.DinhDuong;
using API_WebApplication.Responses.DiemDanh;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Services.DinhDuongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace API_WebApplication.Controllers.DiemDanhs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemDanhController : BaseApiController
    {
        private readonly IDiemDanhService _diemDanhService;

        public DiemDanhController(IDiemDanhService diemDanhService)
        {
            this._diemDanhService = diemDanhService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getDiemDanhsResponse = await _diemDanhService.GetDiemDanhByUser(UserID_Protected);
            if (!getDiemDanhsResponse.Success)
            {
                return UnprocessableEntity(getDiemDanhsResponse);
            }

            var tasksResponse = getDiemDanhsResponse.DiemDanhs.ConvertAll(o => new DiemDanhModel
            {
                Id = o.Id,
                UserId = o.UserId,
                Role = o.Role,
                StudentId = o.StudentId,
                Content = o.Content,
                DenLop = o.DenLop,
                CoPhep = o.CoPhep,
                KhongPhep = o.KhongPhep,
                CreateDate = o.CreateDate,
                IsCompleted = o.IsCompleted,

            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("statusupdate")]
        public async Task<IActionResult> GetAllStatusUpdate(string day, string month, string year)
        {
            var getDiemDanhsResponse = await _diemDanhService.GetDiemDanhStatus(UserID_Protected, day, month, year);
            if (!getDiemDanhsResponse.Success)
            {
                return UnprocessableEntity(getDiemDanhsResponse);
            }

            var tasksResponse = getDiemDanhsResponse.DiemDanhs.ConvertAll(o => new DiemDanhModel
            {
                Id = o.Id,
                UserId = o.UserId,
                Role = o.Role,
                StudentId = o.StudentId,
                Content = o.Content,
                DenLop = o.DenLop,
                CoPhep = o.CoPhep,
                KhongPhep = o.KhongPhep,
                CreateDate = o.CreateDate,
                IsCompleted = o.IsCompleted,

            });
            return Ok(tasksResponse);
        }


        [Authorize]
        [HttpGet("studentId")]
        public async Task<IActionResult> GetDiemDanhByDateAndIDStudent(int StudentID)
        {
            var getDiemDanhsResponse = await _diemDanhService.GetDiemDanhByDateAndIDStudent(StudentID, UserID_Protected);
            if (!getDiemDanhsResponse.Success)
            {
                return UnprocessableEntity(getDiemDanhsResponse);
            }

            var tasksResponse = new DiemDanhModel
            {
                Id = getDiemDanhsResponse.DiemDanh.Id,
                UserId = getDiemDanhsResponse.DiemDanh.UserId,
                Role = getDiemDanhsResponse.DiemDanh.Role,
                StudentId = getDiemDanhsResponse.DiemDanh.StudentId,
                Content = getDiemDanhsResponse.DiemDanh.Content,
                DenLop = getDiemDanhsResponse.DiemDanh.DenLop,
                CoPhep = getDiemDanhsResponse.DiemDanh.CoPhep,
                KhongPhep = getDiemDanhsResponse.DiemDanh.KhongPhep,
                CreateDate = getDiemDanhsResponse.DiemDanh.CreateDate,
                IsCompleted = getDiemDanhsResponse.DiemDanh.IsCompleted,

            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getDiemDanhResponse = await _diemDanhService.GetIDDiemDanh(UserID, StudentID);
            if (!getDiemDanhResponse.Success)
            {
                return UnprocessableEntity(getDiemDanhResponse);
            }

            var tasksResponse = new DiemDanhModel
            {
                Id = getDiemDanhResponse.DiemDanh.Id,
                UserId = getDiemDanhResponse.DiemDanh.UserId,
                Role = getDiemDanhResponse.DiemDanh.Role,
                StudentId = getDiemDanhResponse.DiemDanh.StudentId,
                Content = getDiemDanhResponse.DiemDanh.Content,
                DenLop = getDiemDanhResponse.DiemDanh.DenLop,
                CoPhep = getDiemDanhResponse.DiemDanh.CoPhep,
                KhongPhep = getDiemDanhResponse.DiemDanh.KhongPhep,
                CreateDate = getDiemDanhResponse.DiemDanh.CreateDate,
                IsCompleted = getDiemDanhResponse.DiemDanh.IsCompleted,
            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(DiemDanhRequest diemDanhRequest)
        {
            DateTime dateTime = DateTime.Now;
            var dinhduong = new DiemDanhModel
            {
                StudentId = diemDanhRequest.StudentId,
                Content = diemDanhRequest.Content,
                DenLop = diemDanhRequest.DenLop,
                CoPhep = diemDanhRequest.CoPhep,
                KhongPhep = diemDanhRequest.KhongPhep,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = diemDanhRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = diemDanhRequest.AppID
            };
            var saveDinhDuongResponse = await _diemDanhService.SaveDiemDanh(dinhduong);
            if (!saveDinhDuongResponse.Success)
            {
                return UnprocessableEntity(saveDinhDuongResponse);
            }
            var taskResponse = new DiemDanhModel
            {
                Id = saveDinhDuongResponse.DiemDanh.Id,
                UserId = saveDinhDuongResponse.DiemDanh.UserId,
                Role = saveDinhDuongResponse.DiemDanh.Role,
                StudentId = saveDinhDuongResponse.DiemDanh.StudentId,
                Content = saveDinhDuongResponse.DiemDanh.Content,
                DenLop = saveDinhDuongResponse.DiemDanh.DenLop,
                CoPhep = saveDinhDuongResponse.DiemDanh.CoPhep,
                KhongPhep = saveDinhDuongResponse.DiemDanh.KhongPhep,
                CreateDate = saveDinhDuongResponse.DiemDanh.CreateDate,
                IsCompleted = saveDinhDuongResponse.DiemDanh.IsCompleted,
                AppID = saveDinhDuongResponse.DiemDanh.AppID,
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpPost("postmultiple")]
        public async Task<IActionResult> PostMultiple(List<DiemDanhRequest> diemDanhRequest)
        {
            DateTime dateTime = DateTime.Now;
            List<DiemDanhModel> _data = new List<DiemDanhModel>();
            //List<DiemDanhRequest> diemDanhConvert = (List<DiemDanhRequest>)JsonConvert.DeserializeObject(diemDanhRequest.ToString(), typeof(List<DiemDanhRequest>));
            for (int i = 0; i < diemDanhRequest.Count;i++)
            {
                _data.Add(new DiemDanhModel {
                    StudentId = diemDanhRequest[i].StudentId,
                    Content = diemDanhRequest[i].Content,
                    DenLop = diemDanhRequest[i].DenLop,
                    CoPhep = diemDanhRequest[i].CoPhep,
                    KhongPhep = diemDanhRequest[i].KhongPhep,
                    CreateDate = dateTime,
                    Role = 0,
                    IsCompleted = diemDanhRequest[i].IsCompleted,
                    UserId = UserID_Protected,
                    AppID = diemDanhRequest[i].AppID
                });
            }
            //var dinhduong = new DiemDanhModel
            //{
            //    StudentId = diemDanhRequest.StudentId,
            //    Content = diemDanhRequest.Content,
            //    DenLop = diemDanhRequest.DenLop,
            //    CoPhep = diemDanhRequest.CoPhep,
            //    KhongPhep = diemDanhRequest.KhongPhep,
            //    CreateDate = dateTime,
            //    Role = 0,
            //    IsCompleted = diemDanhRequest.IsCompleted,
            //    UserId = UserID_Protected,
            //    AppID = diemDanhRequest.AppID
            //};
            var saveDinhDuongResponse = await _diemDanhService.SaveDiemDanhMultiple(_data, UserID_Protected);
            if (!saveDinhDuongResponse.Success)
            {
                return UnprocessableEntity(saveDinhDuongResponse);
            }

            return Ok(saveDinhDuongResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteDiemDanhResponse = await _diemDanhService.DeleteDiemDanh(id, UserID_Protected);
            if (!deleteDiemDanhResponse.Success)
            {
                return UnprocessableEntity(deleteDiemDanhResponse);
            }
            return Ok(deleteDiemDanhResponse.DiemDanhId);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiemDanhRequest diemDanhUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var diemdanh = new DiemDanhModel
            {
                StudentId = diemDanhUpdateRequest.StudentId,
                Content = diemDanhUpdateRequest.Content,
                DenLop = diemDanhUpdateRequest.DenLop,
                CoPhep = diemDanhUpdateRequest.CoPhep,
                KhongPhep = diemDanhUpdateRequest.KhongPhep,
                UpdateDate = dateTime,
                IsCompleted = diemDanhUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = diemDanhUpdateRequest.AppID
            };
            var saveDiemDanhResponse = await _diemDanhService.UpdateDiemDanh(id, diemdanh);
            if (!saveDiemDanhResponse.Success)
            {
                return UnprocessableEntity(saveDiemDanhResponse);
            }
            var taskResponse = new DiemDanhModel
            {
                Id = saveDiemDanhResponse.DiemDanh.Id,
                UserId = saveDiemDanhResponse.DiemDanh.UserId,
                Role = saveDiemDanhResponse.DiemDanh.Role,
                StudentId = saveDiemDanhResponse.DiemDanh.StudentId,
                Content = saveDiemDanhResponse.DiemDanh.Content,
                DenLop = saveDiemDanhResponse.DiemDanh.DenLop,
                CoPhep = saveDiemDanhResponse.DiemDanh.CoPhep,
                KhongPhep = saveDiemDanhResponse.DiemDanh.KhongPhep,
                UpdateDate = dateTime,
                IsCompleted = saveDiemDanhResponse.DiemDanh.IsCompleted,
                AppID = saveDiemDanhResponse.DiemDanh.AppID
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpPut("updatemultiple")]
        public async Task<IActionResult> PutMultiple(List<DiemDanhRequest> diemDanhUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;

            var saveDiemDanhResponse = await _diemDanhService.UpdateDiemDanhMultiple(UserID_Protected,diemDanhUpdateRequest);
            if (!saveDiemDanhResponse.Success)
            {
                return UnprocessableEntity(saveDiemDanhResponse);
            }
            return Ok(saveDiemDanhResponse);
        }
    }
}
