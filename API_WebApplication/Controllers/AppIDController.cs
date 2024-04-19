using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.AppIDs;
using API_WebApplication.Interfaces.TinTuc;
using API_WebApplication.Models;
using API_WebApplication.Requests.AppID;
using API_WebApplication.Requests.TinTuc;
using API_WebApplication.Responses.AppIDs;
using API_WebApplication.Responses.TinTuc;
using API_WebApplication.Services.TinTucs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppIDController : BaseApiController
    {
        private readonly IAppIDService _iAppIDService;
        public AppIDController(IAppIDService iAppIDService)
        {
            this._iAppIDService = iAppIDService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getTinTucsResponse = await _iAppIDService.GetAppIDByUser(UserID_Protected);
            if (!getTinTucsResponse.Success)
            {
                return UnprocessableEntity(getTinTucsResponse);
            }

            var tasksResponse = getTinTucsResponse.appIDs.ConvertAll(o => new AppID
            {
                Id = o.Id,
                Name = o.Name,
                IPServer = o.IPServer,
                Logo = o.Logo,
                Address = o.Address,
                UserId = o.UserId,
                Field1 = o.Field1,
                Field2 = o.Field2,
                Field3 = o.Field3,
                Field4 = o.Field4,
                Field5 = o.Field5,
                Field6 = o.Field6,
                Field7 = o.Field7,
                Field8 = o.Field8,
                Field9 = o.Field9,
                Field10 = o.Field10,
                Status = o.Status,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,

            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int TinTucID)
        {
            var getAppIDResponse = await _iAppIDService.GetIDAppID(UserID_Protected, TinTucID);
            if (!getAppIDResponse.Success)
            {
                return UnprocessableEntity(getAppIDResponse);
            }

            var tasksResponse = new AppID
            {
                Id = getAppIDResponse.appID.Id,
                Name = getAppIDResponse.appID.Name,
                IPServer = getAppIDResponse.appID.IPServer,
                Logo = getAppIDResponse.appID.Logo,
                Address = getAppIDResponse.appID.Address,
                UserId = getAppIDResponse.appID.UserId,
                Field1 = getAppIDResponse.appID.Field1,
                Field2 = getAppIDResponse.appID.Field2,
                Field3 = getAppIDResponse.appID.Field3,
                Field4 = getAppIDResponse.appID.Field4,
                Field5 = getAppIDResponse.appID.Field5,
                Field6 = getAppIDResponse.appID.Field6,
                Field7 = getAppIDResponse.appID.Field7,
                Field8 = getAppIDResponse.appID.Field8,
                Field9 = getAppIDResponse.appID.Field9,
                Field10 = getAppIDResponse.appID.Field10,
                Status = getAppIDResponse.appID.Status,
                CreateDate = getAppIDResponse.appID.CreateDate,
                UpdateDate = getAppIDResponse.appID.UpdateDate,
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(AppIDRequest appIDRequest)
        {
            DateTime dateTime = DateTime.Now;
            var tintuc = new AppID
            {
                
                Name = appIDRequest.Name == null ? "" : appIDRequest.Name,
                IPServer = appIDRequest.IPServer == null ? "": appIDRequest.IPServer,
                Logo = appIDRequest.Logo == null ? "" : appIDRequest.Logo,
                Address = appIDRequest.Address == null ? "" : appIDRequest.Address,
                UserId = UserID_Protected.ToString(),
                Field1 = appIDRequest.Field1 == null ? "" : appIDRequest.Field1,
                Field2 = appIDRequest.Field2 == null ? "" : appIDRequest.Field2,
                Field3 = appIDRequest.Field3 == null ? "" : appIDRequest.Field3,
                Field4 = appIDRequest.Field4 == null ? "" : appIDRequest.Field4,
                Field5 = appIDRequest.Field5 == null ? "" : appIDRequest.Field5,
                Field6 = appIDRequest.Field6 == null ? "" : appIDRequest.Field6,
                Field7 = appIDRequest.Field7 == null ? "" : appIDRequest.Field7,
                Field8 = appIDRequest.Field8 == null ? "" : appIDRequest.Field8,
                Field9 = appIDRequest.Field9 == null ? "" : appIDRequest.Field9,
                Field10 = appIDRequest.Field10 == null ? "" : appIDRequest.Field10,
                Status = appIDRequest.Status,
                CreateDate = dateTime
            };
            var saveAppIDResponse = await _iAppIDService.SaveAppID(tintuc);
            if (!saveAppIDResponse.Success)
            {
                return UnprocessableEntity(saveAppIDResponse);
            }
            var taskResponse = new AppID
            {
                Id = saveAppIDResponse.appID.Id,
                Name = saveAppIDResponse.appID.Name,
                IPServer = saveAppIDResponse.appID.IPServer,
                Logo = saveAppIDResponse.appID.Logo,
                Address = saveAppIDResponse.appID.Address,
                UserId = saveAppIDResponse.appID.UserId,
                Field1 = saveAppIDResponse.appID.Field1,
                Field2 = saveAppIDResponse.appID.Field2,
                Field3 = saveAppIDResponse.appID.Field3,
                Field4 = saveAppIDResponse.appID.Field4,
                Field5 = saveAppIDResponse.appID.Field5,
                Field6 = saveAppIDResponse.appID.Field6,
                Field7 = saveAppIDResponse.appID.Field7,
                Field8 = saveAppIDResponse.appID.Field8,
                Field9 = saveAppIDResponse.appID.Field9,
                Field10 = saveAppIDResponse.appID.Field10,
                Status = saveAppIDResponse.appID.Status,
                CreateDate = saveAppIDResponse.appID.CreateDate,
                UpdateDate = saveAppIDResponse.appID.UpdateDate,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AppIDRequest appIDRequest)
        {
            DateTime dateTime = DateTime.Now;
            var appID = new AppID
            {
                Name = appIDRequest.Name,
                IPServer = appIDRequest.IPServer,
                Logo = appIDRequest.Logo,
                Address = appIDRequest.Address,
                UserId = UserID_Protected.ToString(),
                Field1 = appIDRequest.Field1,
                Field2 = appIDRequest.Field2,
                Field3 = appIDRequest.Field3,
                Field4 = appIDRequest.Field4,
                Field5 = appIDRequest.Field5,
                Field6 = appIDRequest.Field6,
                Field7 = appIDRequest.Field7,
                Field8 = appIDRequest.Field8,
                Field9 = appIDRequest.Field9,
                Field10 = appIDRequest.Field10,
                Status = appIDRequest.Status,
                UpdateDate = dateTime
            };
            var saveAppIDResponse = await _iAppIDService.UpdateAppID(id, appID);
            if (!saveAppIDResponse.Success)
            {
                return UnprocessableEntity(saveAppIDResponse);
            }
            var taskResponse = new AppID
            {
                Id = saveAppIDResponse.appID.Id,
                Name = saveAppIDResponse.appID.Name,
                IPServer = saveAppIDResponse.appID.IPServer,
                Logo = saveAppIDResponse.appID.Logo,
                Address = saveAppIDResponse.appID.Address,
                UserId = saveAppIDResponse.appID.UserId,
                Field1 = saveAppIDResponse.appID.Field1,
                Field2 = saveAppIDResponse.appID.Field2,
                Field3 = saveAppIDResponse.appID.Field3,
                Field4 = saveAppIDResponse.appID.Field4,
                Field5 = saveAppIDResponse.appID.Field5,
                Field6 = saveAppIDResponse.appID.Field6,
                Field7 = saveAppIDResponse.appID.Field7,
                Field8 = saveAppIDResponse.appID.Field8,
                Field9 = saveAppIDResponse.appID.Field9,
                Field10 = saveAppIDResponse.appID.Field10,
                Status = saveAppIDResponse.appID.Status,
                CreateDate = saveAppIDResponse.appID.CreateDate,
                UpdateDate = saveAppIDResponse.appID.UpdateDate,
            };

            return Ok(taskResponse);
        }
    }
}
