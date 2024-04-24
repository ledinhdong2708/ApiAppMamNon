using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.MaterBieuDos;
using API_WebApplication.Interfaces.MaterHocPhis;
using API_WebApplication.Models;
using API_WebApplication.Requests.MaterBieuDo;
using API_WebApplication.Requests.MaterChiPhi;
using API_WebApplication.Responses.MaterBieuDos;
using API_WebApplication.Services.MaterBieuDos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.MaterHocPhis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterHocPhisController : BaseApiController
    {
        private readonly IMaterHocPhiService _materHocPhiService;

        public MaterHocPhisController(IMaterHocPhiService materHocPhiService)
        {
            this._materHocPhiService = materHocPhiService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getMaterBieuDosResponse = await _materHocPhiService.GetMaterHocPhiByUser(UserID_Protected);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterHocPhis.ConvertAll(o => new MaterHocPhi
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                Content = o.Content,
                DonViTinh = o.DonViTinh,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("statusFalse")]
        public async Task<IActionResult> GetAllHocPhiStatusFalse()
        {
            var getMaterBieuDosResponse = await _materHocPhiService.GetMaterHocPhiFalseByUser(UserID_Protected);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterHocPhis.ConvertAll(o => new MaterHocPhi
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                Content = o.Content,
                DonViTinh = o.DonViTinh,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byStudent")]
        public async Task<IActionResult> GetAllByStudent(int classID)
        {
            var getMaterBieuDosResponse = await _materHocPhiService.GetMaterHocPhiByStudent(UserID_Protected, classID);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterHocPhis.ConvertAll(o => new MaterHocPhi
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                Content = o.Content,
                DonViTinh = o.DonViTinh,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getMaterBieuDosResponse = await _materHocPhiService.GetIDMaterHocPhi(UserID, StudentID);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = new MaterHocPhi
            {
                Id = getMaterBieuDosResponse.MaterHocPhi.Id,
                Content = getMaterBieuDosResponse.MaterHocPhi.Content,
                DonViTinh = getMaterBieuDosResponse.MaterHocPhi.DonViTinh,
                CreateDate = getMaterBieuDosResponse.MaterHocPhi.CreateDate,
                IsCompleted = getMaterBieuDosResponse.MaterHocPhi.IsCompleted,
                StudentId = getMaterBieuDosResponse.MaterHocPhi.StudentId,
                AppID = getMaterBieuDosResponse.MaterHocPhi.AppID
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(MaterHocPhiRequest materHocPhiRequest)
        {
            DateTime dateTime = DateTime.Now;
            var materHocPhi = new MaterHocPhi
            {
                Content = materHocPhiRequest.Content,
                DonViTinh = materHocPhiRequest.DonViTinh,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = materHocPhiRequest.IsCompleted,
                StudentId = materHocPhiRequest.StudentId,
                UserId = UserID_Protected,
                AppID = materHocPhiRequest.AppID
            };
            var saveMaterBieuDoResponse = await _materHocPhiService.SaveMaterHocPhi(materHocPhi);
            if (!saveMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(saveMaterBieuDoResponse);
            }
            var taskResponse = new MaterHocPhi
            {
                Id = saveMaterBieuDoResponse.MaterHocPhi.Id,
                Content = saveMaterBieuDoResponse.MaterHocPhi.Content,
                DonViTinh = saveMaterBieuDoResponse.MaterHocPhi.DonViTinh,
                CreateDate = saveMaterBieuDoResponse.MaterHocPhi.CreateDate,
                IsCompleted = saveMaterBieuDoResponse.MaterHocPhi.IsCompleted,
                StudentId = saveMaterBieuDoResponse.MaterHocPhi.StudentId,
                AppID = saveMaterBieuDoResponse.MaterHocPhi.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteMaterBieuDoResponse = await _materHocPhiService.DeleteMaterHocPhi(id, UserID_Protected);
            if (!deleteMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(deleteMaterBieuDoResponse);
            }
            return Ok(deleteMaterBieuDoResponse.MaterHocPhiId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterHocPhiRequest materHocPhiUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var materHocPhi = new MaterHocPhi
            {
                Content = materHocPhiUpdateRequest.Content,
                DonViTinh = materHocPhiUpdateRequest.DonViTinh,
                UpdateDate = dateTime,
                IsCompleted = materHocPhiUpdateRequest.IsCompleted,
                StudentId = materHocPhiUpdateRequest.StudentId,
                UserId = UserID_Protected,
                AppID = materHocPhiUpdateRequest.AppID
            };
            var saveMaterBieuDoResponse = await _materHocPhiService.UpdateMaterHocPhi(id, materHocPhi);
            if (!saveMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(saveMaterBieuDoResponse);
            }
            var taskResponse = new MaterHocPhi
            {
                Id = saveMaterBieuDoResponse.MaterHocPhi.Id,
                Content = saveMaterBieuDoResponse.MaterHocPhi.Content,
                DonViTinh = saveMaterBieuDoResponse.MaterHocPhi.DonViTinh,
                CreateDate = saveMaterBieuDoResponse.MaterHocPhi.CreateDate,
                IsCompleted = saveMaterBieuDoResponse.MaterHocPhi.IsCompleted,
                StudentId = saveMaterBieuDoResponse.MaterHocPhi.StudentId,
                AppID = saveMaterBieuDoResponse.MaterHocPhi.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("ph/byId")]
        public async Task<IActionResult> PH_GetByStudent(int id)
        {
            var getMaterBieuDosResponse = await _materHocPhiService.PH_GetMaterHocPhiByStudent(id, UserID_Protected);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterHocPhis.ConvertAll(o => new MaterHocPhi
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                Content = o.Content,
                DonViTinh = o.DonViTinh,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
    }
}
