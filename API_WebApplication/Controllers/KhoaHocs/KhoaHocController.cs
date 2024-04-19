using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.KhoaHocs;
using API_WebApplication.Models;
using API_WebApplication.Requests.KhoaHoc;
using API_WebApplication.Services.KhoaHocs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.KhoaHocs
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : BaseApiController
    {
        private readonly IKhoaHocService _KhoaHocService;

        public KhoaHocController(IKhoaHocService khoahocService)
        {
            this._KhoaHocService = khoahocService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getKhoaHocsResponse = await _KhoaHocService.GetKhoaHocByUser(UserID_Protected);
            if (!getKhoaHocsResponse.Success)
            {
                return UnprocessableEntity(getKhoaHocsResponse);
            }

            var tasksResponse = getKhoaHocsResponse.KhoaHocs.ConvertAll(o => new KhoaHocModel
            {
                Id = o.Id,
                FromYear = o.FromYear,
                ToYear = o.ToYear,
                IsCompleted = o.IsCompleted,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,

            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int KhoaHocID)
        {
            var getKhoaHocResponse = await _KhoaHocService.GetIDKhoaHoc(UserID, KhoaHocID);
            if (!getKhoaHocResponse.Success)
            {
                return UnprocessableEntity(getKhoaHocResponse);
            }

            var tasksResponse = new KhoaHocModel
            {
                Id = getKhoaHocResponse.KhoaHoc.Id,
                FromYear = getKhoaHocResponse.KhoaHoc.FromYear,
                ToYear = getKhoaHocResponse.KhoaHoc.ToYear,
                CreateDate = getKhoaHocResponse.KhoaHoc.CreateDate,
                UpdateDate = getKhoaHocResponse.KhoaHoc.UpdateDate,
                IsCompleted = getKhoaHocResponse.KhoaHoc.IsCompleted,


            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(KhoaHocRequest KhoaHocRequest)
        {
            DateTime dateTime = DateTime.Now;
            var KhoaHoc = new KhoaHocModel
            {
                FromYear = KhoaHocRequest.FromYear,
                ToYear = KhoaHocRequest.ToYear,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = KhoaHocRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = KhoaHocRequest.AppID
            };
            var saveKhoaHocResponse = await _KhoaHocService.SaveKhoaHoc(KhoaHoc);
            if (!saveKhoaHocResponse.Success)
            {
                return UnprocessableEntity(saveKhoaHocResponse);
            }
            var taskResponse = new KhoaHocModel
            {
                Id = saveKhoaHocResponse.KhoaHoc.Id,
                FromYear = saveKhoaHocResponse.KhoaHoc.FromYear,
                ToYear = saveKhoaHocResponse.KhoaHoc.ToYear,
                CreateDate = saveKhoaHocResponse.KhoaHoc.CreateDate,
                IsCompleted = saveKhoaHocResponse.KhoaHoc.IsCompleted,
                AppID = saveKhoaHocResponse.KhoaHoc.AppID
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteKhoaHocResponse = await _KhoaHocService.DeleteKhoaHoc(id, UserID_Protected);
            if (!deleteKhoaHocResponse.Success)
            {
                return UnprocessableEntity(deleteKhoaHocResponse);
            }
            return Ok(deleteKhoaHocResponse.KhoaHocId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, KhoaHocRequest KhoaHocUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var KhoaHoc = new KhoaHocModel
            {
                FromYear = KhoaHocUpdateRequest.FromYear,
                ToYear = KhoaHocUpdateRequest.ToYear,
                UpdateDate = dateTime,
                IsCompleted = KhoaHocUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = KhoaHocUpdateRequest.AppID
            };
            var savKhoaHocResponse = await _KhoaHocService.UpdateKhoaHoc(id, KhoaHoc);
            if (!savKhoaHocResponse.Success)
            {
                return UnprocessableEntity(savKhoaHocResponse);
            }
            var taskResponse = new KhoaHocModel
            {
                Id = savKhoaHocResponse.KhoaHoc.Id,
                FromYear = savKhoaHocResponse.KhoaHoc.FromYear,
                ToYear = savKhoaHocResponse.KhoaHoc.ToYear,
                UpdateDate = dateTime,
                IsCompleted = savKhoaHocResponse.KhoaHoc.IsCompleted,
                AppID = savKhoaHocResponse.KhoaHoc.AppID
            };

            return Ok(taskResponse);
        }
    }
}
