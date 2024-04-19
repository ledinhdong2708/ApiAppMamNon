using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.MaterBieuDos;
using API_WebApplication.Models;
using API_WebApplication.Requests.DinhDuong;
using API_WebApplication.Requests.MaterBieuDo;
using API_WebApplication.Services.DinhDuongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.MaterBieuDos
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterBieuDoController : BaseApiController
    {
        private readonly IMaterBieuDoService _materBieuDoService;

        public MaterBieuDoController(IMaterBieuDoService materBieuDoService)
        {
            this._materBieuDoService = materBieuDoService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getMaterBieuDosResponse = await _materBieuDoService.GetMaterBieuDoByUser(UserID_Protected);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterBieuDos.ConvertAll(o => new MaterBieuDo
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                BMI = o.BMI,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byStudent")]
        public async Task<IActionResult> GetAllByStudent(int classID)
        {
            var getMaterBieuDosResponse = await _materBieuDoService.GetMaterBieuDoByStudent(UserID_Protected, classID);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterBieuDos.ConvertAll(o => new MaterBieuDo
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                BMI = o.BMI,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getMaterBieuDosResponse = await _materBieuDoService.GetIDMaterBieuDo(UserID, StudentID);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = new MaterBieuDo
            {
                Id = getMaterBieuDosResponse.MaterBieuDo.Id,
                DocDate = getMaterBieuDosResponse.MaterBieuDo.DocDate,
                CanNang = getMaterBieuDosResponse.MaterBieuDo.CanNang,
                ChieuCao = getMaterBieuDosResponse.MaterBieuDo.ChieuCao,
                BMI = getMaterBieuDosResponse.MaterBieuDo.BMI,
                CreateDate = getMaterBieuDosResponse.MaterBieuDo.CreateDate,
                IsCompleted = getMaterBieuDosResponse.MaterBieuDo.IsCompleted,
                StudentId = getMaterBieuDosResponse.MaterBieuDo.StudentId
            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(MaterBieuDoRequest materBieuDoRequest)
        {
            DateTime dateTime = DateTime.Now;
            var materBieuDo = new MaterBieuDo
            {
                DocDate = materBieuDoRequest.DocDate,
                CanNang = materBieuDoRequest.CanNang,
                ChieuCao = materBieuDoRequest.ChieuCao,
                BMI = materBieuDoRequest.BMI,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = materBieuDoRequest.IsCompleted,
                StudentId = materBieuDoRequest.StudentId,
                UserId = UserID_Protected,
                AppID = materBieuDoRequest.AppID
            };
            var saveMaterBieuDoResponse = await _materBieuDoService.SaveMaterBieuDo(materBieuDo);
            if (!saveMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(saveMaterBieuDoResponse);
            }
            var taskResponse = new MaterBieuDo
            {
                Id = saveMaterBieuDoResponse.MaterBieuDo.Id,
                DocDate = saveMaterBieuDoResponse.MaterBieuDo.DocDate,
                CanNang = saveMaterBieuDoResponse.MaterBieuDo.CanNang,
                ChieuCao = saveMaterBieuDoResponse.MaterBieuDo.ChieuCao,
                BMI = saveMaterBieuDoResponse.MaterBieuDo.BMI,
                CreateDate = saveMaterBieuDoResponse.MaterBieuDo.CreateDate,
                IsCompleted = saveMaterBieuDoResponse.MaterBieuDo.IsCompleted,
                StudentId = saveMaterBieuDoResponse.MaterBieuDo.StudentId,
                AppID = saveMaterBieuDoResponse.MaterBieuDo.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteMaterBieuDoResponse = await _materBieuDoService.DeleteMaterBieuDo(id, UserID_Protected);
            if (!deleteMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(deleteMaterBieuDoResponse);
            }
            return Ok(deleteMaterBieuDoResponse.MaterBieuDoId);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterBieuDoRequest materBieuDoUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var materBieuDo = new MaterBieuDo
            {
                DocDate = materBieuDoUpdateRequest.DocDate,
                CanNang = materBieuDoUpdateRequest.CanNang,
                ChieuCao = materBieuDoUpdateRequest.ChieuCao,
                BMI = materBieuDoUpdateRequest.BMI,
                UpdateDate = dateTime,
                IsCompleted = materBieuDoUpdateRequest.IsCompleted,
                StudentId = materBieuDoUpdateRequest.StudentId,
                UserId = UserID_Protected,
                AppID = materBieuDoUpdateRequest.AppID
            };
            var saveMaterBieuDoResponse = await _materBieuDoService.UpdateMaterBieuDo(id, materBieuDo);
            if (!saveMaterBieuDoResponse.Success)
            {
                return UnprocessableEntity(saveMaterBieuDoResponse);
            }
            var taskResponse = new MaterBieuDo
            {
                Id = saveMaterBieuDoResponse.MaterBieuDo.Id,
                DocDate = saveMaterBieuDoResponse.MaterBieuDo.DocDate,
                ChieuCao = saveMaterBieuDoResponse.MaterBieuDo.ChieuCao,
                CanNang = saveMaterBieuDoResponse.MaterBieuDo.CanNang,
                BMI = saveMaterBieuDoResponse.MaterBieuDo.BMI,
                UpdateDate = dateTime,
                IsCompleted = saveMaterBieuDoResponse.MaterBieuDo.IsCompleted,
                StudentId = saveMaterBieuDoResponse.MaterBieuDo.StudentId,
                AppID = saveMaterBieuDoResponse.MaterBieuDo.AppID
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("ph/byId")]
        public async Task<IActionResult> PH_GetByStudent(int id)
        {
            var getMaterBieuDosResponse = await _materBieuDoService.PH_GetMaterBieuDoByStudent(id, UserID_Protected);
            if (!getMaterBieuDosResponse.Success)
            {
                return UnprocessableEntity(getMaterBieuDosResponse);
            }

            var tasksResponse = getMaterBieuDosResponse.MaterBieuDos.ConvertAll(o => new MaterBieuDo
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                BMI = o.BMI,
                StudentId = o.StudentId,
                CreateDate = o.CreateDate
            });
            return Ok(tasksResponse);
        }
    }
}
