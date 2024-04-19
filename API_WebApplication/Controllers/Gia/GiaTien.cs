using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.GiaTiens;
using API_WebApplication.Models;
using API_WebApplication.Requests.GiaTiens;
using API_WebApplication.Responses;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.GiaTiens;
using API_WebApplication.Responses.HoatDong;
using API_WebApplication.Services.DinhDuongs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_WebApplication.Controllers.Gia
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaTien : BaseApiController
    {

        private readonly IGiaTienService _giaTienService;

        public GiaTien(IGiaTienService _giaTienService)
        {
            this._giaTienService = _giaTienService;
        }
        [HttpPost("NewGiaTiens")]
        public async Task<IActionResult> NewGiaTien(int id, NewGiaTiensResquest newGiaTiensResquest)
        {
            DateTime date = DateTime.Now;

            var Giatien = new GiaTiensModel
            {
                gia = newGiaTiensResquest.gia,
                name = newGiaTiensResquest.name,
                CreateDate = date,
                UpdateDate = date,
            };

            var newgiatien = await _giaTienService.NewGiatiensResponse(id, Giatien);

            if (newgiatien.Success)
            {
                return UnprocessableEntity(newgiatien);
            }

            var taskResponse = new GiaTiensModel
            {
                gia = newgiatien.GiaTiensModel.gia,
                name = newgiatien.GiaTiensModel.name,
                CreateDate = date,
            };

            return Ok(taskResponse);
        }

        [HttpGet("GetAllItem")]
        public async Task<IActionResult> GetAllItem()
        {
            var getall = await _giaTienService.getallItemResponse();
            if (getall == null)
            {
                return UnprocessableEntity(getall);
            }
            var tasksResponse = getall.giaTiensModels.ConvertAll(o => new GiaTiensModel
            {
                Id = o.Id,
                name = o.name,
                gia = o.gia,
                CreateDate = o.CreateDate,
                 UpdateDate = o.CreateDate,
            });
            return Ok(tasksResponse);
        }
        [HttpPut("UpdateGiaTien/{id}")]
        public async Task<IActionResult> UpdateGiaTien(int id, GiaTiensResquest gia)
        {
            var giatien = new GiaTiensModel
            {
                gia = gia.gia,
                UpdateDate = DateTime.Now,

            };

            var updatedGiaTien = await _giaTienService.UpdateGiaTiensResponse(id, giatien);
            if (!updatedGiaTien.Success)
            {
                return UnprocessableEntity(updatedGiaTien);
            }
            var taskResponse = new GiaTiensModel
            {
                UpdateDate= DateTime.Now,
                gia = updatedGiaTien.giaTiensModels.gia,
             
            };
            return Ok(taskResponse);
        }

    }
}
