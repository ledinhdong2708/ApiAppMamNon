using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.TableLikeInterface;
using API_WebApplication.Models;
using API_WebApplication.Requests.DinhDuong;
using API_WebApplication.Requests.NhatKy;
using API_WebApplication.Services.DinhDuongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.TableLikesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableLikeController : BaseApiController
    {
        private readonly ITableLikeService _tableLikeService;

        public TableLikeController(ITableLikeService tableLikeService)
        {
            this._tableLikeService = tableLikeService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(TableLikeRequest tableLikeRequest)
        {
            DateTime dateTime = DateTime.Now;
            var tableLike = new TableLike
            {
                Content = tableLikeRequest.Content,
                CreateDate = dateTime,
                Status = tableLikeRequest.Status,
                NhatKyId = tableLikeRequest.NhatKyId,
                UserId = UserID_Protected,
                StudentId = tableLikeRequest.StudentId,
                AppID = tableLikeRequest.AppID,
            };
            var saveDinhDuongResponse = await _tableLikeService.SaveTableLike(tableLike);
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
            var deleteDinhDuongResponse = await _tableLikeService.DeleteTableLike(id, UserID_Protected);
            if (!deleteDinhDuongResponse.Success)
            {
                return UnprocessableEntity(deleteDinhDuongResponse);
            }
            return Ok(deleteDinhDuongResponse.TableLikeId);
        }
    }
}
