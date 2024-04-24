using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.TableBinhLuanInterface;
using API_WebApplication.Interfaces.TableLikeInterface;
using API_WebApplication.Models;
using API_WebApplication.Requests.TableBinhLuan;
using API_WebApplication.Services.TableLikeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.TableBinhLuanController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableBinhLuansController : BaseApiController
    {
        private readonly ITableBinhLuanService _tableBinhLuanService;
        public TableBinhLuansController(ITableBinhLuanService tableBinhLuanService)
        {
            this._tableBinhLuanService = tableBinhLuanService;
        }
        [Authorize]
        [HttpGet("byidnhatky")]
        public async Task<IActionResult> GetTableBinhLuanByIDNhatKy(int NhatKyId)
        {
            var getDinhDuongResponse = await _tableBinhLuanService.GetTableBinhLuanByIDNhatKy(NhatKyId, UserID_Protected);
            if (!getDinhDuongResponse.Success)
            {
                return UnprocessableEntity(getDinhDuongResponse);
            }
            return Ok(getDinhDuongResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(TableBinhLuanRequest tableBinhLuanRequest)
        {
            DateTime dateTime = DateTime.Now;
            var tableBinhLuan = new BinhLuan
            {
                Content = tableBinhLuanRequest.Content,
                CreateDate = dateTime,
                Status = tableBinhLuanRequest.Status,
                NhatKyId = tableBinhLuanRequest.NhatKyId,
                UserId = UserID_Protected,
                StudentId = tableBinhLuanRequest.StudentId,
                AppID = tableBinhLuanRequest.AppID,
            };
            var saveDinhDuongResponse = await _tableBinhLuanService.SaveTableBinhLuan(tableBinhLuan);
            if (!saveDinhDuongResponse.Success)
            {
                return UnprocessableEntity(saveDinhDuongResponse);
            }
            return Ok(saveDinhDuongResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteDinhDuongResponse = await _tableBinhLuanService.DeleteTableBinhLuan(id, UserID_Protected);
            if (!deleteDinhDuongResponse.Success)
            {
                return UnprocessableEntity(deleteDinhDuongResponse);
            }
            return Ok(deleteDinhDuongResponse.TableBinhLuanId);
        }
    }
}
