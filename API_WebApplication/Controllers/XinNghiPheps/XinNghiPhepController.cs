using API_WebApplication.Controllers.Bases;
using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Interfaces.HocPhis;
using API_WebApplication.Interfaces.XinNghiPheps;
using API_WebApplication.Models;
using API_WebApplication.Requests.HocPhi;
using API_WebApplication.Requests.XinNghiPhep;
using API_WebApplication.Services.HocPhis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.XinNghiPheps
{
    [Route("api/[controller]")]
    [ApiController]
    public class XinNghiPhepController : BaseApiController
    {
        private readonly IXinNghiPhepService _xinNghiPhepService;
        public XinNghiPhepController(IXinNghiPhepService xinNghiPhepService)
        {
            this._xinNghiPhepService = xinNghiPhepService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getXinNghiPhepResponse = await _xinNghiPhepService.GetXinNghiPhepsByUser(UserID_Protected);
            if (!getXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(getXinNghiPhepResponse);
            }

            return Ok(getXinNghiPhepResponse);
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int UserID, int XinNghiPhepID)
        {
            var getXinNghiPhepResponse = await _xinNghiPhepService.GetIDXinNghiPhep(UserID, XinNghiPhepID);
            if (!getXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(getXinNghiPhepResponse);
            }
            return Ok(getXinNghiPhepResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(XinNghiPhepDTO _xinnghiphepdto)
        {
            _xinnghiphepdto.UserId = UserID_Protected;
            var getHocPhiResponse = await _xinNghiPhepService.SaveXinNghiPhep(_xinnghiphepdto);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPost("ph")]
        public async Task<IActionResult> PH_Post(XinNghiPhepDTO _xinnghiphepdto)
        {
            _xinnghiphepdto.UserId = 1;
            var getHocPhiResponse = await _xinNghiPhepService.PH_SaveXinNghiPhep(_xinnghiphepdto);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, XinNghiPhepDTO _xinnghiphepdto)
        {
            var updateXinNghiPhepResponse = await _xinNghiPhepService.UpdateXinNghiPhep(id, _xinnghiphepdto);
            if (!updateXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(updateXinNghiPhepResponse);
            }
            return Ok(updateXinNghiPhepResponse);
        }

        [Authorize]
        [HttpPut("ph/{id}")]
        public async Task<IActionResult> PH_Put(int id, XinNghiPhepDTO _xinnghiphepdto)
        {
            var updateXinNghiPhepResponse = await _xinNghiPhepService.PH_UpdateXinNghiPhep(id, _xinnghiphepdto);
            if (!updateXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(updateXinNghiPhepResponse);
            }
            return Ok(updateXinNghiPhepResponse);
        }
        [Authorize]
        [HttpPut("xinnghiphep_detail/{xinnghiphep_id:int}")]
        public async Task<IActionResult> Put_HocPhi_Detail(int id, ChiTietXinNghiPhep _xinnghiphepdto)
        {
            DateTime dateTime = DateTime.Now;
            var xinNghiPhep_Detail = new ChiTietXinNghiPhep
            {
                Content = _xinnghiphepdto.Content,
                StudentId = _xinnghiphepdto.StudentId,
                UserId = UserID_Protected,
                UpdateDate = dateTime,
                FromDate = _xinnghiphepdto.FromDate,
                ToDate = _xinnghiphepdto.ToDate,
                AppID = _xinnghiphepdto.AppID

            };
            var updateXinNghiPhepResponse = await _xinNghiPhepService.UpdateXinNghiPhep_Detail(id, xinNghiPhep_Detail);

            if (!updateXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(updateXinNghiPhepResponse);
            }

            var taskResponse = new ChiTietXinNghiPhep
            {
                Content = updateXinNghiPhepResponse.XinNghiPhep.Content,
                StudentId = updateXinNghiPhepResponse.XinNghiPhep.StudentId,
                UpdateDate = dateTime,
                FromDate = updateXinNghiPhepResponse.XinNghiPhep.FromDate,
                ToDate = updateXinNghiPhepResponse.XinNghiPhep.ToDate,
                AppID = updateXinNghiPhepResponse.XinNghiPhep.AppID
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpPut("ph/xinnghiphep_detail/{xinnghiphep_id:int}")]
        public async Task<IActionResult> PH_Put_HocPhi_Detail(int id, ChiTietXinNghiPhep _xinnghiphepdto)
        {
            DateTime dateTime = DateTime.Now;
            var xinNghiPhep_Detail = new ChiTietXinNghiPhep
            {
                Content = _xinnghiphepdto.Content,
                StudentId = _xinnghiphepdto.StudentId,
                UserId = UserID_Protected,
                UpdateDate = dateTime,
                FromDate = _xinnghiphepdto.FromDate,
                ToDate = _xinnghiphepdto.ToDate,
                AppID = _xinnghiphepdto.AppID

            };
            var updateXinNghiPhepResponse = await _xinNghiPhepService.PH_UpdateXinNghiPhep_Detail(id, xinNghiPhep_Detail);

            if (!updateXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(updateXinNghiPhepResponse);
            }

            var taskResponse = new ChiTietXinNghiPhep
            {
                Content = updateXinNghiPhepResponse.XinNghiPhep.Content,
                StudentId = updateXinNghiPhepResponse.XinNghiPhep.StudentId,
                UpdateDate = dateTime,
                FromDate = updateXinNghiPhepResponse.XinNghiPhep.FromDate,
                ToDate = updateXinNghiPhepResponse.XinNghiPhep.ToDate,
                AppID = updateXinNghiPhepResponse.XinNghiPhep.AppID
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("date")]
        public async Task<IActionResult> GetIDDinhDuongByDate(int UserID, int day, int month, int year)
        {
            var getXinNghiPhepResponse = await _xinNghiPhepService.GetIDXinNghiPhepByDate(UserID, day, month, year);
            if (!getXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(getXinNghiPhepResponse);
            }
            return Ok(getXinNghiPhepResponse);
        }
        [Authorize]
        [HttpGet("ph")]
        public async Task<IActionResult> PH_GetIDByHocPhiStudent(int id)
        {
            var getXinNghiPhepResponse = await _xinNghiPhepService.PH_GetIDXinNghiPhepByStudent(id, UserID_Protected);
            if (!getXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(getXinNghiPhepResponse);
            }

            return Ok(getXinNghiPhepResponse);
        }
    }
}
