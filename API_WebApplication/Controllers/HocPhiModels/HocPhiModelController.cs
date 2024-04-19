using API_WebApplication.Controllers.Bases;
using API_WebApplication.DTO.HocPhiModel;
using API_WebApplication.Interfaces.HocPhiModels;
using API_WebApplication.Models;
using API_WebApplication.Requests.HocPhiModel;
using API_WebApplication.Responses.TinTuc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.HocPhiModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhiModelController : BaseApiController
    {
        private readonly IHocPhiModelService _hocPhiModelService;
        public HocPhiModelController(IHocPhiModelService hocPhiModelService)
        {
            this._hocPhiModelService = hocPhiModelService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getHocPhiResponse = await _hocPhiModelService.GetHocPhiModelsByUser(UserID_Protected);
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
            var getHocPhiResponse = await _hocPhiModelService.GetIDHocPhiModel(UserID, HocPhiID);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(HocPhiModel2DTO _hocphidto)
        {
            _hocphidto.UserId = UserID_Protected;
            var getHocPhiResponse = await _hocPhiModelService.SaveHocPhiModel(_hocphidto);
            if (!getHocPhiResponse.Success)
            {
                return UnprocessableEntity(getHocPhiResponse);
            }
            return Ok(getHocPhiResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, HocPhiModel2DTO _hocphidto)
        {
            var updateHocPhiResponse = await _hocPhiModelService.UpdateHocPhiModel(id, _hocphidto);
            if (!updateHocPhiResponse.Success)
            {
                return UnprocessableEntity(updateHocPhiResponse);
            }
            return Ok(updateHocPhiResponse);
        }

        [Authorize]
        [HttpPut("hocphi_detail")]
        public async Task<IActionResult> Put_HocPhi_Detail(int id, HocPhiModelRequest _hocphidto)
        {
            DateTime dateTime = DateTime.Now;
            var hocPhi_Detail = new HocPhiModel2
            {
                Content = _hocphidto.Content,
                StudentId = _hocphidto.StudentId,
                UserId = UserID_Protected,
                UpdateDate = dateTime,
                Months = _hocphidto.Months,
                Years = _hocphidto.Years,
                Total = _hocphidto.Total,
                PhiOff = _hocphidto.PhiOff,
                ConLai = _hocphidto.ConLai,
                IsCompleted = _hocphidto.IsCompleted,
                AppID = _hocphidto.AppID,
            };
            var updateHocPhiResponse = await _hocPhiModelService.UpdateHocPhiModel_Detail(id, hocPhi_Detail);

            if (!updateHocPhiResponse.Success)
            {
                return UnprocessableEntity(updateHocPhiResponse);
            }
            
            var taskResponse = new HocPhiModel2
            {
                Content = updateHocPhiResponse.HocPhiModel.Content,
                StudentId = updateHocPhiResponse.HocPhiModel.StudentId,
                UpdateDate = dateTime,
                UserId = UserID_Protected,
                Months = updateHocPhiResponse.HocPhiModel.Months,
                Years = updateHocPhiResponse.HocPhiModel.Years,
                Total = updateHocPhiResponse.HocPhiModel.Total,
                PhiOff = updateHocPhiResponse.HocPhiModel.PhiOff,
                ConLai = updateHocPhiResponse.HocPhiModel.ConLai,
                IsCompleted = updateHocPhiResponse.HocPhiModel.IsCompleted,
                Status = updateHocPhiResponse.HocPhiModel.Status,
                AppID = updateHocPhiResponse.HocPhiModel.AppID
            };
            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("byStudent")]
        public async Task<IActionResult> GetAllByStudent(int id)
        {
            var getMaterBieuDosResponse = await _hocPhiModelService.GetHocPhiModelByStudent(UserID_Protected, id);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            //var tasksResponse = getMaterBieuDosResponse.HocPhiModels.ConvertAll(o => new HocPhiModel2
            //{
            //    Id = o.Id,
            //    IsCompleted = o.IsCompleted,
            //    Months = o.Months,
            //    Years = o.Years,
            //    Total = o.Total,
            //    PhiOff = o.PhiOff,
            //    ConLai = o.ConLai,
            //    StudentId = o.StudentId,
            //    CreateDate = o.CreateDate
            //});
            return Ok(getMaterBieuDosResponse);
        }
        [Authorize]
        [HttpGet("ph/byStudent")]
        public async Task<IActionResult> PH_GetAllByStudent(int id)
        {
            var getMaterBieuDosResponse = await _hocPhiModelService.PH_GetHocPhiModelByStudent(UserID_Protected, id);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            //var tasksResponse = getMaterBieuDosResponse.HocPhiModels.ConvertAll(o => new HocPhiModel2
            //{
            //    Id = o.Id,
            //    IsCompleted = o.IsCompleted,
            //    Months = o.Months,
            //    Years = o.Years,
            //    Total = o.Total,
            //    PhiOff = o.PhiOff,
            //    ConLai = o.ConLai,
            //    StudentId = o.StudentId,
            //    CreateDate = o.CreateDate
            //});
            return Ok(getMaterBieuDosResponse);
        }
        [Authorize]
        [HttpPut("statusHocPhi/{statusHocPhi}")]
        public async Task<IActionResult> Put(int id, bool statusHocPhi)
        {
            var savTinTucResponse = await _hocPhiModelService.UpdateStatusHocPhi(id, statusHocPhi);
            if (!savTinTucResponse.Success)
            {
                return UnprocessableEntity(savTinTucResponse);
            }
            var taskResponse = new HocPhiModel2
            {
                Status = statusHocPhi,
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpPost("newHocPhiAll")]
        public async Task<IActionResult> newHocPhiAll (string selectedClassId, string selectedKhoaHoc, decimal tienHocPhiController, string selectedMonth, decimal tienAnController, string selectedYearString)
        {
            var newHocphi = await _hocPhiModelService.newHocPhiModel2Response(selectedClassId, selectedKhoaHoc, tienHocPhiController, selectedMonth, UserID_Protected, tienAnController, selectedYearString);
            if (!newHocphi.Success)
            {
                return UnprocessableEntity(newHocphi);
            }

            return Ok (newHocphi);
        }

    }
}
