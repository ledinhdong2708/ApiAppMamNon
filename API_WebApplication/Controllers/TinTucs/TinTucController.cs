using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.TinTuc;
using API_WebApplication.Models;
using API_WebApplication.Requests.DinhDuong;
using API_WebApplication.Requests.TinTuc;
using API_WebApplication.Services.DinhDuongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.TinTucs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinTucController : BaseApiController
    {
        private readonly ITinTucService _tintucService;

        public TinTucController(ITinTucService tintucService)
        {
            this._tintucService = tintucService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getTinTucsResponse = await _tintucService.GetTinTucByUser(UserID_Protected);
            if (!getTinTucsResponse.Success)
            {
                return UnprocessableEntity(getTinTucsResponse);
            }

            var tasksResponse = getTinTucsResponse.TinTucs.ConvertAll(o => new TinTucModel
            {
                Id = o.Id,
                Title = o.Title,
                Content = o.Content,
                IsCompleted = o.IsCompleted,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int TinTucID)
        {
            var getTinTucResponse = await _tintucService.GetIDTinTuc(UserID, TinTucID);
            if (!getTinTucResponse.Success)
            {
                return UnprocessableEntity(getTinTucResponse);
            }

            var tasksResponse = new TinTucModel
            {
                Id = getTinTucResponse.TinTuc.Id,
                Title = getTinTucResponse.TinTuc.Title,
                Content = getTinTucResponse.TinTuc.Content,
                CreateDate = getTinTucResponse.TinTuc.CreateDate,
                UpdateDate = getTinTucResponse.TinTuc.UpdateDate,
                IsCompleted = getTinTucResponse.TinTuc.IsCompleted,


            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(TinTucRequest tintucRequest)
        {
            DateTime dateTime = DateTime.Now;
            var tintuc = new TinTucModel
            {
                Title = tintucRequest.Title,
                Content = tintucRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = tintucRequest.IsCompleted,
                AppID = tintucRequest.AppID,
                UserId = UserID_Protected
            };
            var saveTinTucResponse = await _tintucService.SaveTinTuc(tintuc);
            if (!saveTinTucResponse.Success)
            {
                return UnprocessableEntity(saveTinTucResponse);
            }
            var taskResponse = new TinTucModel
            {
                Id = saveTinTucResponse.TinTuc.Id,
                Title = saveTinTucResponse.TinTuc.Title,
                Content = saveTinTucResponse.TinTuc.Content,
                CreateDate = saveTinTucResponse.TinTuc.CreateDate,
                IsCompleted = saveTinTucResponse.TinTuc.IsCompleted,
                AppID =saveTinTucResponse.TinTuc.AppID,
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteTinTucResponse = await _tintucService.DeleteTinTuc(id, UserID_Protected);
            if (!deleteTinTucResponse.Success)
            {
                return UnprocessableEntity(deleteTinTucResponse);
            }
            return Ok(deleteTinTucResponse.TinTucId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TinTucRequest tintucUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var tintuc = new TinTucModel
            {
                Title = tintucUpdateRequest.Title,
                Content = tintucUpdateRequest.Content,
                UpdateDate = dateTime,
                IsCompleted = tintucUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = tintucUpdateRequest.AppID,
            };
            var savTinTucResponse = await _tintucService.UpdateTinTuc(id, tintuc);
            if (!savTinTucResponse.Success)
            {
                return UnprocessableEntity(savTinTucResponse);
            }
            var taskResponse = new TinTucModel
            {
                Id = savTinTucResponse.TinTuc.Id,
               Title = savTinTucResponse.TinTuc.Title,
                Content = savTinTucResponse.TinTuc.Content,
                UpdateDate = dateTime,
                IsCompleted = savTinTucResponse.TinTuc.IsCompleted,
                AppID = savTinTucResponse.TinTuc.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("ph")]
        public async Task<IActionResult> PH_GetAll()
        {
            var getTinTucsResponse = await _tintucService.PH_GetTinTucBy(UserID_Protected);
            if (!getTinTucsResponse.Success)
            {
                return UnprocessableEntity(getTinTucsResponse);
            }

            var tasksResponse = getTinTucsResponse.TinTucs.ConvertAll(o => new TinTucModel
            {
                Id = o.Id,
                Title = o.Title,
                Content = o.Content,
                IsCompleted = o.IsCompleted,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,

            });
            return Ok(tasksResponse);
        }
    }
}
