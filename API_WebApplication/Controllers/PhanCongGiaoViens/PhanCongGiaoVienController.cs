using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.PhanCongGiaoVienModels;
using API_WebApplication.Models;
using API_WebApplication.Requests.PhanCongGiaoVien;
using API_WebApplication.Services.PhanCongGiaoViens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.PhanCongGiaoViens
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongGiaoVienController : BaseApiController
    {
        private readonly IPhanCongGiaoVienService _PhanCongGiaoVienService;
        public PhanCongGiaoVienController(IPhanCongGiaoVienService phanCongGiaoVienService)
        {
            _PhanCongGiaoVienService = phanCongGiaoVienService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(string classid, string khoahocid)
        {
            var getPhanCongGiaoViensResponse = await _PhanCongGiaoVienService.GetPhanCongGiaoVienByUser(UserID_Protected, classid,khoahocid);
            if (!getPhanCongGiaoViensResponse.Success)
            {
                return UnprocessableEntity(getPhanCongGiaoViensResponse);
            }

            var tasksResponse = getPhanCongGiaoViensResponse.PhanCongGiaoViens.ConvertAll(o => new PhanCongGiaoVienModel
            {
                Id = o.Id,
                Status = o.Status,
                KhoaHocId = o.KhoaHocId,
                Content = o.Content,
                ClassId = o.ClassId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byStudent")]
        public async Task<IActionResult> GetAllByStudent(int id)
        {
            var getPhanCongGiaoViensResponse = await _PhanCongGiaoVienService.GetPhanCongGiaoVienByStudent(UserID_Protected, id);
            if (!getPhanCongGiaoViensResponse.Success)
            {
                return UnprocessableEntity(getPhanCongGiaoViensResponse);
            }

            var tasksResponse = getPhanCongGiaoViensResponse.PhanCongGiaoViens.ConvertAll(o => new PhanCongGiaoVienModel
            {
                Id = o.Id,
                Status = o.Status,
                KhoaHocId = o.KhoaHocId,
                Content = o.Content,
                ClassId = o.ClassId,
                CreateDate = o.CreateDate,
                AppID = o.AppID,
                UserAdd = o.UserAdd
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getPhanCongGiaoViensResponse = await _PhanCongGiaoVienService.GetIDPhanCongGiaoVien(UserID, StudentID);
            if (!getPhanCongGiaoViensResponse.Success)
            {
                return UnprocessableEntity(getPhanCongGiaoViensResponse);
            }

            var tasksResponse = new PhanCongGiaoVienModel
            {
                Id = getPhanCongGiaoViensResponse.PhanCongGiaoVien.Id,
                KhoaHocId = getPhanCongGiaoViensResponse.PhanCongGiaoVien.KhoaHocId,
                Content = getPhanCongGiaoViensResponse.PhanCongGiaoVien.Content,
                CreateDate = getPhanCongGiaoViensResponse.PhanCongGiaoVien.CreateDate,
                Status = getPhanCongGiaoViensResponse.PhanCongGiaoVien.Status,
                ClassId = getPhanCongGiaoViensResponse.PhanCongGiaoVien.ClassId,
                UserAdd = getPhanCongGiaoViensResponse.PhanCongGiaoVien.UserAdd,
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(PhanCongGiaoVienRequest PhanCongGiaoVienRequest)
        {
            DateTime dateTime = DateTime.Now;
            var PhanCongGiaoVien = new PhanCongGiaoVienModel
            {
                KhoaHocId = PhanCongGiaoVienRequest.KhoaHocId,
                Content = PhanCongGiaoVienRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                Status = PhanCongGiaoVienRequest.Status,
                ClassId = PhanCongGiaoVienRequest.ClassId,
                UserId = UserID_Protected,
                AppID = PhanCongGiaoVienRequest.AppID,
                UserAdd = PhanCongGiaoVienRequest.UserAdd,
                IsCompleted = PhanCongGiaoVienRequest.IsCompleted
            };
            var savePhanCongGiaoVienResponse = await _PhanCongGiaoVienService.SavePhanCongGiaoVien(PhanCongGiaoVien);
            if (!savePhanCongGiaoVienResponse.Success)
            {
                return UnprocessableEntity(savePhanCongGiaoVienResponse);
            }
            var taskResponse = new PhanCongGiaoVienModel
            {
                Id = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Id,
                KhoaHocId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.KhoaHocId,
                Content = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Content,
                CreateDate = savePhanCongGiaoVienResponse.PhanCongGiaoVien.CreateDate,
                Status = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Status,
                ClassId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.ClassId,
                AppID = savePhanCongGiaoVienResponse.PhanCongGiaoVien.AppID,
                UserAdd = savePhanCongGiaoVienResponse.PhanCongGiaoVien.UserAdd,
                IsCompleted = savePhanCongGiaoVienResponse.PhanCongGiaoVien.IsCompleted,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPost("ph")]
        public async Task<IActionResult> PH_Post(PhanCongGiaoVienRequest PhanCongGiaoVienRequest)
        {
            DateTime dateTime = DateTime.Now;
            var PhanCongGiaoVien = new PhanCongGiaoVienModel
            {
                KhoaHocId = PhanCongGiaoVienRequest.KhoaHocId,
                Content = PhanCongGiaoVienRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                Status = PhanCongGiaoVienRequest.Status,
                ClassId = PhanCongGiaoVienRequest.ClassId,
                UserId = UserID_Protected,
                AppID = PhanCongGiaoVienRequest.AppID,
                UserAdd = PhanCongGiaoVienRequest?.UserAdd,
                IsCompleted = (bool)(PhanCongGiaoVienRequest?.IsCompleted),
            };
            var savePhanCongGiaoVienResponse = await _PhanCongGiaoVienService.PH_SavePhanCongGiaoVien(PhanCongGiaoVien);
            if (!savePhanCongGiaoVienResponse.Success)
            {
                return UnprocessableEntity(savePhanCongGiaoVienResponse);
            }
            var taskResponse = new PhanCongGiaoVienModel
            {
                Id = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Id,
                KhoaHocId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.KhoaHocId,
                Content = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Content,
                CreateDate = savePhanCongGiaoVienResponse.PhanCongGiaoVien.CreateDate,
                Status = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Status,
                ClassId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.ClassId,
                AppID = savePhanCongGiaoVienResponse.PhanCongGiaoVien.AppID,
                UserAdd = savePhanCongGiaoVienResponse.PhanCongGiaoVien.UserAdd,
                IsCompleted = savePhanCongGiaoVienResponse.PhanCongGiaoVien.IsCompleted,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletePhanCongGiaoVienResponse = await _PhanCongGiaoVienService.DeletePhanCongGiaoVien(id, UserID_Protected);
            if (!deletePhanCongGiaoVienResponse.Success)
            {
                return UnprocessableEntity(deletePhanCongGiaoVienResponse);
            }
            return Ok(deletePhanCongGiaoVienResponse.PhanCongGiaoVienId);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PhanCongGiaoVienRequest daThuocUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var PhanCongGiaoVien = new PhanCongGiaoVienModel
            {
                KhoaHocId = daThuocUpdateRequest.KhoaHocId,
                Content = daThuocUpdateRequest.Content,
                UpdateDate = dateTime,
                Status = daThuocUpdateRequest.Status,
                ClassId = daThuocUpdateRequest.ClassId,
                UserId = UserID_Protected,
                AppID = daThuocUpdateRequest.AppID,
                UserAdd = daThuocUpdateRequest?.UserAdd,

            };
            var savePhanCongGiaoVienResponse = await _PhanCongGiaoVienService.UpdatePhanCongGiaoVien(id, PhanCongGiaoVien);
            if (!savePhanCongGiaoVienResponse.Success)
            {
                return UnprocessableEntity(savePhanCongGiaoVienResponse);
            }
            var taskResponse = new PhanCongGiaoVienModel
            {
                Id = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Id,
                KhoaHocId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.KhoaHocId,
                Content = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Content,
                UpdateDate = dateTime,
                Status = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Status,
                ClassId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.ClassId,
                AppID = savePhanCongGiaoVienResponse.PhanCongGiaoVien?.AppID,
                UserAdd = savePhanCongGiaoVienResponse.PhanCongGiaoVien?.UserAdd,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPut("ph/{id}")]
        public async Task<IActionResult> PH_Put(int id, PhanCongGiaoVienRequest daThuocUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var PhanCongGiaoVien = new PhanCongGiaoVienModel
            {
                Content = daThuocUpdateRequest.Content,
                UpdateDate = dateTime,
                Status = daThuocUpdateRequest.Status,
                ClassId = daThuocUpdateRequest.ClassId,
                KhoaHocId = daThuocUpdateRequest.KhoaHocId,
                UserId = UserID_Protected,
                AppID = daThuocUpdateRequest.AppID,
                UserAdd = daThuocUpdateRequest?.UserAdd,
            };
            var savePhanCongGiaoVienResponse = await _PhanCongGiaoVienService.PH_UpdatePhanCongGiaoVien(id, PhanCongGiaoVien);
            if (!savePhanCongGiaoVienResponse.Success)
            {
                return UnprocessableEntity(savePhanCongGiaoVienResponse);
            }
            var taskResponse = new PhanCongGiaoVienModel
            {
                Id = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Id,
                Content = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Content,
                UpdateDate = dateTime,
                Status = savePhanCongGiaoVienResponse.PhanCongGiaoVien.Status,
                ClassId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.ClassId,
                KhoaHocId = savePhanCongGiaoVienResponse.PhanCongGiaoVien.KhoaHocId,
                AppID = savePhanCongGiaoVienResponse.PhanCongGiaoVien.AppID,
                UserAdd = savePhanCongGiaoVienResponse.PhanCongGiaoVien?.UserAdd,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("ph/byStudent")]
        public async Task<IActionResult> PH_GetAllByStudent(int studentId)
        {
            var getPhanCongGiaoViensResponse = await _PhanCongGiaoVienService.PH_GetPhanCongGiaoVienByStudent(UserID_Protected, studentId);
            if (!getPhanCongGiaoViensResponse.Success)
            {
                return UnprocessableEntity(getPhanCongGiaoViensResponse);
            }

            var tasksResponse = getPhanCongGiaoViensResponse.PhanCongGiaoViens.ConvertAll(o => new PhanCongGiaoVienModel
            {
                Id = o.Id,
                Status = o.Status,
                Content = o.Content,
                ClassId = o.ClassId,
                KhoaHocId = o.KhoaHocId,
                CreateDate = o.CreateDate,
                UserAdd = o.UserAdd
                
            });
            return Ok(tasksResponse);
        }
    }
}
