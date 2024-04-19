using API_WebApplication.Controllers.Bases;
using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.Interfaces.ThoiKhoaBieu;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Responses.Students;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Controllers.ThoiKhoaBieu
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiKhoaBieuControllerExample : BaseApiController
    {
        //private readonly API_Application_V1Context _aPI_Application_V1Context;
        //private readonly IMapper _mapper;
        //public ThoiKhoaBieuController(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        //{
        //    this._aPI_Application_V1Context = aPI_Application_V1Context;
        //    this._mapper = mapper;
        //}
        private readonly IThoiKhoaBieuServiceExample _thoiKhoaBieuService;
        //public ThoiKhoaBieuController(IThoiKhoaBieuService thoiKhoaBieuService, API_Application_V1Context aPI_Application_V1Context,IMapper mapper)
        //{
        //    this._thoiKhoaBieuService = thoiKhoaBieuService;
        //    this._aPI_Application_V1Context = aPI_Application_V1Context;
        //    this._mapper = mapper;
        //}
        public ThoiKhoaBieuControllerExample(IThoiKhoaBieuServiceExample thoiKhoaBieuService)
        {
            this._thoiKhoaBieuService = thoiKhoaBieuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getThoiKhoaBieuResponse = await _thoiKhoaBieuService.GetThoiKhoaBieusByUser(UserID_Protected);
            if (!getThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieuResponse);
            }

            return Ok(getThoiKhoaBieuResponse);
            //var thoiKhoaBieu = await _aPI_Application_V1Context.Otkb.Include(_ => _.Tkb1).ToListAsync();
            //return Ok(thoiKhoaBieu);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int UserID, int ThoiKhoaBieuID)
        {
            var getThoiKhoaBieuResponse = await _thoiKhoaBieuService.GetIDThoiKhoaBieu(UserID, ThoiKhoaBieuID);
            if (!getThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieuResponse);
            }
            return Ok(getThoiKhoaBieuResponse);
            //var thoiKhoaBieu = await _aPI_Application_V1Context.Otkb.Include(_ => _.Tkb1).Where(_ => _.Id == id).FirstOrDefaultAsync();
            //return Ok(thoiKhoaBieu);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OTKBDTO _otkbdto)
        {
            _otkbdto.UserId = UserID_Protected;
            var saveThoiKhoaBieuResponse = await _thoiKhoaBieuService.SaveThoiKhoaBieu(_otkbdto);
            if (!saveThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(saveThoiKhoaBieuResponse);
            }
            return Ok(saveThoiKhoaBieuResponse);
            //var newTKB = _mapper.Map<Otkb>(_otkbdto);
            //_aPI_Application_V1Context.Add(newTKB);
            //await _aPI_Application_V1Context.SaveChangesAsync();
            //return Created($"/ThoiKhoaBieu/{newTKB.Id}", newTKB);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,OTKBDTO _otkbdto)
        {
            var updateStudentResponse = await _thoiKhoaBieuService.UpdateThoiKhoaBieu(id, _otkbdto);
            if (!updateStudentResponse.Success)
            {
                return UnprocessableEntity(updateStudentResponse);
            }
            return Ok(updateStudentResponse);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(OTKBDTO _otkbdto)
        //{
        //    var updateOTKB = _mapper.Map<Otkb>(_otkbdto);
        //    _aPI_Application_V1Context.Update(updateOTKB);
        //    await _aPI_Application_V1Context.SaveChangesAsync();
        //    return Ok(updateOTKB);
        //}
    }
}
