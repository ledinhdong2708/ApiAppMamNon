using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DanThuocs;
using API_WebApplication.Interfaces.MaterBieuDos;
using API_WebApplication.Models;
using API_WebApplication.Requests.DanThuoc;
using API_WebApplication.Requests.MaterBieuDo;
using API_WebApplication.Services.DanThuocs;
using API_WebApplication.Services.MaterBieuDos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.DanThuocs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanThuocController : BaseApiController
    {
        private readonly IDanThuocService _danThuocService;
        public DanThuocController(IDanThuocService danThuocService)
        {
            _danThuocService = danThuocService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getDanThuocsResponse = await _danThuocService.GetDanThuocByUser(UserID_Protected);
            if (!getDanThuocsResponse.Success)
            {
                return UnprocessableEntity(getDanThuocsResponse);
            }

            var tasksResponse = getDanThuocsResponse.DanThuocModels.ConvertAll(o => new DanThuocModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                Content = o.Content,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byStudent")]
        public async Task<IActionResult> GetAllByStudent(int classID, string day, string month, string year)
        {
            var getDanThuocsResponse = await _danThuocService.GetDanThuocByStudent(UserID_Protected, classID, day, month, year);
            if (!getDanThuocsResponse.Success)
            {
                return UnprocessableEntity(getDanThuocsResponse);
            }

            var tasksResponse = getDanThuocsResponse.DanThuocModels.ConvertAll(o => new DanThuocModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                Content = o.Content,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getDanThuocsResponse = await _danThuocService.GetIDDanThuoc(UserID, StudentID);
            if (!getDanThuocsResponse.Success)
            {
                return UnprocessableEntity(getDanThuocsResponse);
            }

            var tasksResponse = new DanThuocModel
            {
                Id = getDanThuocsResponse.DanThuoc.Id,
                DocDate = getDanThuocsResponse.DanThuoc.DocDate,
                Content = getDanThuocsResponse.DanThuoc.Content,
                CreateDate = getDanThuocsResponse.DanThuoc.CreateDate,
                IsCompleted = getDanThuocsResponse.DanThuoc.IsCompleted,
                StudentId = getDanThuocsResponse.DanThuoc.StudentId
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(DanThuocRequest danThuocRequest)
        {
            DateTime dateTime = DateTime.Now;
            var danThuoc = new DanThuocModel
            {
                DocDate = danThuocRequest.DocDate,
                Content = danThuocRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = danThuocRequest.IsCompleted,
                StudentId = danThuocRequest.StudentId,
                UserId = UserID_Protected,
                AppID = danThuocRequest.AppID
            };
            var saveDanThuocResponse = await _danThuocService.SaveDanThuoc(danThuoc);
            if (!saveDanThuocResponse.Success)
            {
                return UnprocessableEntity(saveDanThuocResponse);
            }
            var taskResponse = new DanThuocModel
            {
                Id = saveDanThuocResponse.DanThuoc.Id,
                DocDate = saveDanThuocResponse.DanThuoc.DocDate,
                Content = saveDanThuocResponse.DanThuoc.Content,
                CreateDate = saveDanThuocResponse.DanThuoc.CreateDate,
                IsCompleted = saveDanThuocResponse.DanThuoc.IsCompleted,
                StudentId = saveDanThuocResponse.DanThuoc.StudentId,
                AppID = saveDanThuocResponse.DanThuoc.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPost("ph")]
        public async Task<IActionResult> PH_Post(DanThuocRequest danThuocRequest)
        {
            DateTime dateTime = DateTime.Now;
            var danThuoc = new DanThuocModel
            {
                DocDate = danThuocRequest.DocDate,
                Content = danThuocRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = danThuocRequest.IsCompleted,
                StudentId = danThuocRequest.StudentId,
                UserId = UserID_Protected,
                AppID = danThuocRequest.AppID
            };
            var saveDanThuocResponse = await _danThuocService.PH_SaveDanThuoc(danThuoc);
            if (!saveDanThuocResponse.Success)
            {
                return UnprocessableEntity(saveDanThuocResponse);
            }
            var taskResponse = new DanThuocModel
            {
                Id = saveDanThuocResponse.DanThuoc.Id,
                DocDate = saveDanThuocResponse.DanThuoc.DocDate,
                Content = saveDanThuocResponse.DanThuoc.Content,
                CreateDate = saveDanThuocResponse.DanThuoc.CreateDate,
                IsCompleted = saveDanThuocResponse.DanThuoc.IsCompleted,
                StudentId = saveDanThuocResponse.DanThuoc.StudentId,
                AppID = saveDanThuocResponse.DanThuoc.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteDanThuocResponse = await _danThuocService.DeleteDanThuoc(id, UserID_Protected);
            if (!deleteDanThuocResponse.Success)
            {
                return UnprocessableEntity(deleteDanThuocResponse);
            }
            return Ok(deleteDanThuocResponse.DanThuocId);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DanThuocRequest daThuocUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var danThuoc = new DanThuocModel
            {
                DocDate = daThuocUpdateRequest.DocDate,
                Content = daThuocUpdateRequest.Content,
                UpdateDate = dateTime,
                IsCompleted = daThuocUpdateRequest.IsCompleted,
                StudentId = daThuocUpdateRequest.StudentId,
                UserId = UserID_Protected,
                AppID = daThuocUpdateRequest.AppID
            };
            var saveDanThuocResponse = await _danThuocService.UpdateDanThuoc(id, danThuoc);
            if (!saveDanThuocResponse.Success)
            {
                return UnprocessableEntity(saveDanThuocResponse);
            }
            var taskResponse = new DanThuocModel
            {
                Id = saveDanThuocResponse.DanThuocModel.Id,
                DocDate = saveDanThuocResponse.DanThuocModel.DocDate,
                Content = saveDanThuocResponse.DanThuocModel.Content,
                UpdateDate = dateTime,
                IsCompleted = saveDanThuocResponse.DanThuocModel.IsCompleted,
                StudentId = saveDanThuocResponse.DanThuocModel.StudentId,
                AppID = saveDanThuocResponse.DanThuocModel?.AppID,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPut("ph/{id}")]
        public async Task<IActionResult> PH_Put(int id, DanThuocRequest daThuocUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var danThuoc = new DanThuocModel
            {
                DocDate = daThuocUpdateRequest.DocDate,
                Content = daThuocUpdateRequest.Content,
                UpdateDate = dateTime,
                IsCompleted = daThuocUpdateRequest.IsCompleted,
                StudentId = daThuocUpdateRequest.StudentId,
                UserId = UserID_Protected,
                AppID = daThuocUpdateRequest.AppID,
            };
            var saveDanThuocResponse = await _danThuocService.PH_UpdateDanThuoc(id, danThuoc);
            if (!saveDanThuocResponse.Success)
            {
                return UnprocessableEntity(saveDanThuocResponse);
            }
            var taskResponse = new DanThuocModel
            {
                Id = saveDanThuocResponse.DanThuocModel.Id,
                DocDate = saveDanThuocResponse.DanThuocModel.DocDate,
                Content = saveDanThuocResponse.DanThuocModel.Content,
                UpdateDate = dateTime,
                IsCompleted = saveDanThuocResponse.DanThuocModel.IsCompleted,
                StudentId = saveDanThuocResponse.DanThuocModel.StudentId,
                AppID = saveDanThuocResponse.DanThuocModel.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("ph/byStudent")]
        public async Task<IActionResult> PH_GetAllByStudent(int studentId, string day, string month, string year)
        {
            var getDanThuocsResponse = await _danThuocService.PH_GetDanThuocByStudent(UserID_Protected, studentId, day, month, year);
            if (!getDanThuocsResponse.Success)
            {
                return UnprocessableEntity(getDanThuocsResponse);
            }

            var tasksResponse = getDanThuocsResponse.DanThuocModels.ConvertAll(o => new DanThuocModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                Content = o.Content,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
    }

}
