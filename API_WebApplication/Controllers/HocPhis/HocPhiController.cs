using API_WebApplication.Controllers.Bases;
using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.Interfaces.HocPhis;
using API_WebApplication.Interfaces.ThoiKhoaBieu;
using API_WebApplication.Models;
using API_WebApplication.Requests.HocPhi;
using API_WebApplication.Requests.TinTuc;
using API_WebApplication.Responses.DinhDuong;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.HocPhis
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhiController : BaseApiController
    {
        private readonly IHocPhiService _hocphiService;
        public HocPhiController(IHocPhiService hocphiService)
        {
            this._hocphiService = hocphiService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getHocPhiResponse = await _hocphiService.GetHocPhisByUser(UserID_Protected);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }

            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int UserID, int HocPhiID)
        {
            var getHocPhiResponse = await _hocphiService.GetIDHocPhi(UserID, HocPhiID);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpGet("chitiettheomonth")]
        public async Task<IActionResult> GetChiTietHocPhiTheoMonthByIDChiTietHocPhi(int idchitiethocphitheomonth)
        {
            var getHocPhiResponse = await _hocphiService.GetHocPhiChiTietTheoMonthByIDHocPhiChiTiet(idchitiethocphitheomonth, UserID_Protected);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }

            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(HocPhiDTO _hocphidto)
        {
            _hocphidto.UserId = UserID_Protected;
            var getHocPhiResponse = await _hocphiService.SaveHocPhi(_hocphidto);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, HocPhiDTO _hocphidto)
        {
            var updateHocPhiResponse = await _hocphiService.UpdateHocPhi(id, _hocphidto);
            if (!updateHocPhiResponse.Success)
            {
                return UnprocessableEntity(updateHocPhiResponse);
            }
            return Ok(updateHocPhiResponse);
        }
        [Authorize]
        [HttpPut("hocphi_detail")]
        public async Task<IActionResult> Put_HocPhi_Detail(int id, HocPhiRequest _hocphidto)
        {
            DateTime dateTime = DateTime.Now;
            var hocPhi_Detail = new HocPhi
            {
                Content = _hocphidto.Content,
                StudentId = _hocphidto.StudentId,
                UserId = UserID_Protected,
                UpdateDate = dateTime,
                TotalMax = _hocphidto.TotalMax,
                AppID = _hocphidto.AppID,
            };
            var updateHocPhiResponse = await _hocphiService.UpdateHocPhi_Detail(id, hocPhi_Detail);

            if (!updateHocPhiResponse.Success)
            {
                return UnprocessableEntity(updateHocPhiResponse);
            }

            var taskResponse = new HocPhi
            {
                Content = updateHocPhiResponse.HocPhi.Content,
                StudentId = updateHocPhiResponse.HocPhi.StudentId,
                UpdateDate = dateTime,
                TotalMax = updateHocPhiResponse.HocPhi.TotalMax,AppID = updateHocPhiResponse.HocPhi.AppID
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("byyearclassstudent")]
        public async Task<IActionResult> GetAllFillterByUserYearClass(string year, string classId)
        {
            var getSoBeNgoansResponse = await _hocphiService.GetSoBeNgoanFillterByUserKhoaHocClassStudent(UserID_Protected, year, classId);
            if (!getSoBeNgoansResponse.Success)
            {
                return UnprocessableEntity(getSoBeNgoansResponse);
            }

            var tasksResponse = getSoBeNgoansResponse.HocPhis.ConvertAll(o => new HocPhi
            {
                Id = o.Id,
                Content = o.Content,
                StudentId = o.StudentId,
                UserId = o.UserId,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                TotalMax = o.TotalMax,
                TotalMin = o.TotalMin,
                NameStudent = o.NameStudent,
                AppID = o.AppID,
                ChiTietHocPhis = o.ChiTietHocPhis,
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("ph")]
        public async Task<IActionResult> PH_GetIDByHocPhiStudent(int id)
        {
            var getHocPhiResponse = await _hocphiService.PH_GetIDByHocPhiStudent(id, UserID_Protected);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }

        [Authorize]
        [HttpPut("statusHocPhi/{statusHocPhi}")]
        public async Task<IActionResult> Put(int id, bool statusHocPhi)
        {
            var savTinTucResponse = await _hocphiService.UpdateCompleteChiTietHocPhi(id, statusHocPhi);
            if (!savTinTucResponse.Success)
            {
                return UnprocessableEntity(savTinTucResponse);
            }
            var taskResponse = new TinTucModel
            {
                IsCompleted = statusHocPhi,
            };

            return Ok(taskResponse);
        }
    }
}
