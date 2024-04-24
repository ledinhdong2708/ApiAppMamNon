using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.Classs;
using API_WebApplication.Models;
using API_WebApplication.Requests.Classs;
using API_WebApplication.Services.Classs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.Classs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : BaseApiController
    {
        private readonly IClassService _ClassService;

        public ClassController(IClassService classService)
        {
            this._ClassService = classService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getClasssResponse = await _ClassService.GetClasssByUser(UserID_Protected);
            if (!getClasssResponse.Success)
            {
                return UnprocessableEntity(getClasssResponse);
            }

            var tasksResponse = getClasssResponse.Classss.ConvertAll(o => new ClassModel
            {
                Id = o.Id,
                NameClass = o.NameClass,
                IsCompleted = o.IsCompleted,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,

            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int ClassID)
        {
            var getClassResponse = await _ClassService.GetIDClasss(UserID_Protected, ClassID);
            if (!getClassResponse.Success)
            {
                return UnprocessableEntity(getClassResponse);
            }

            var tasksResponse = new ClassModel
            {
                Id = getClassResponse.Classs.Id,
                NameClass = getClassResponse.Classs.NameClass,
                CreateDate = getClassResponse.Classs.CreateDate,
                UpdateDate = getClassResponse.Classs.UpdateDate,
                IsCompleted = getClassResponse.Classs.IsCompleted,


            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ClasssRequest ClassRequest)
        {
            DateTime dateTime = DateTime.Now;
            var Class = new ClassModel
            {
                NameClass = ClassRequest.NameClass,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = ClassRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = ClassRequest.AppID
                //AppID = ClassRequest.AppID
            };
            var saveClassResponse = await _ClassService.SaveClasss(Class);
            if (!saveClassResponse.Success)
            {
                return UnprocessableEntity(saveClassResponse);
            }
            var taskResponse = new ClassModel
            {
                Id = saveClassResponse.Classs.Id,
                NameClass = saveClassResponse.Classs.NameClass,
                CreateDate = saveClassResponse.Classs.CreateDate,
                IsCompleted = saveClassResponse.Classs.IsCompleted,
                AppID = saveClassResponse.Classs.AppID
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteClassResponse = await _ClassService.DeleteClasss(id, UserID_Protected);
            if (!deleteClassResponse.Success)
            {
                return UnprocessableEntity(deleteClassResponse);
            }
            return Ok(deleteClassResponse.ClassId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClasssRequest ClassUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var Class = new ClassModel
            {
                NameClass = ClassUpdateRequest.NameClass,
                UpdateDate = dateTime,
                IsCompleted = ClassUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = ClassUpdateRequest.AppID,
            };
            var savClassResponse = await _ClassService.UpdateClasss(id, Class);
            if (!savClassResponse.Success)
            {
                return UnprocessableEntity(savClassResponse);
            }
            var taskResponse = new ClassModel
            {
                Id = savClassResponse.Classs.Id,
                NameClass = savClassResponse.Classs.NameClass,
                UpdateDate = dateTime,
                IsCompleted = savClassResponse.Classs.IsCompleted,
                AppID = savClassResponse.Classs.AppID
            };

            return Ok(taskResponse);
        }
    }
}
