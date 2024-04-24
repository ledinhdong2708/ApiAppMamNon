using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.HoatDongs;
using API_WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_WebApplication.Requests.HoatDongModel;
using API_WebApplication.Requests.Students;

namespace API_WebApplication.Controllers.HoatDongs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoatDongController : BaseApiController
    {
        private readonly IHoatDongService _hoatDongService;

        public HoatDongController(IHoatDongService hoatDongService)
        {
            this._hoatDongService = hoatDongService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getHoatDongsResponse = await _hoatDongService.GetHoatDongByUser(UserID_Protected);
            if (!getHoatDongsResponse.Success)
            {
                return UnprocessableEntity(getHoatDongsResponse);
            }

            //var tasksResponse = getHoatDongsResponse.HoatDongModels.ConvertAll(o => new HoatDongModel
            //{
            //    Id = o.Id,
            //    Content = o.Content,
            //    IsCompleted = o.IsCompleted,
            //    CreateDate = o.CreateDate,
            //    UpdateDate = o.UpdateDate,
            //    Months = o.Months,
            //    Years = o.Years,
            //    Img = o.Img,
            //    AppID = o.AppID,
            //    ClassID = o.ClassID,
            //    KhoaHocId = o.KhoaHocId,
            //});
            return Ok(getHoatDongsResponse);
        }
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int HoatDongID)
        {
            var getHoatDongResponse = await _hoatDongService.GetIDHoatDong(UserID, HoatDongID);
            if (!getHoatDongResponse.Success)
            {
                return UnprocessableEntity(getHoatDongResponse);
            }

            //var tasksResponse = new HoatDongModel
            //{
            //    Id = getHoatDongResponse.HoatDong.Id,
            //    Content = getHoatDongResponse.HoatDong.Content,
            //    CreateDate = getHoatDongResponse.HoatDong.CreateDate,
            //    UpdateDate = getHoatDongResponse.HoatDong.UpdateDate,
            //    IsCompleted = getHoatDongResponse.HoatDong.IsCompleted,
            //    Months = getHoatDongResponse.HoatDong.Months,
            //    Years = getHoatDongResponse.HoatDong.Years,
            //    Img = getHoatDongResponse.HoatDong.Img,
            //    AppID = getHoatDongResponse.HoatDong.AppID,
            //    ClassID = getHoatDongResponse.HoatDong.ClassID,
            //};
            return Ok(getHoatDongResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(HoatDongModelRespuest HoatDongRequest)
        {
            DateTime dateTime = DateTime.Now;
            
            // Upload File
            var uniqueFileName = Guid.NewGuid().ToString();
            var HoatDong = new HoatDongModel
            {
                Content = HoatDongRequest.Content,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = HoatDongRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = HoatDongRequest.AppID,
                ClassID = HoatDongRequest.ClassID,
                KhoaHocId = HoatDongRequest.KhoaHocId,
                Img = HoatDongRequest.Img
            };
            // End Upload File
            var saveHoatDongResponse = await _hoatDongService.SaveHoatDong(HoatDong);
            if (!saveHoatDongResponse.Success)
            {
                return UnprocessableEntity(saveHoatDongResponse);
            }
            var taskResponse = new HoatDongModel
            {
                Id = saveHoatDongResponse.HoatDong.Id,
                Content = saveHoatDongResponse.HoatDong.Content,
                CreateDate = saveHoatDongResponse.HoatDong.CreateDate,
                IsCompleted = saveHoatDongResponse.HoatDong.IsCompleted,
                UserId = UserID_Protected,
                AppID = saveHoatDongResponse.HoatDong.AppID,
                Months = saveHoatDongResponse.HoatDong.Months,
                Years = saveHoatDongResponse.HoatDong.Years,
                ClassID = saveHoatDongResponse?.HoatDong.ClassID,
                KhoaHocId = saveHoatDongResponse?.HoatDong?.KhoaHocId,
                Img = saveHoatDongResponse?.HoatDong.Img
            };
            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteHoatDongResponse = await _hoatDongService.DeleteHoatDong(id, UserID_Protected);
            if (!deleteHoatDongResponse.Success)
            {
                return UnprocessableEntity(deleteHoatDongResponse);
            }
            return Ok(deleteHoatDongResponse.HoatDongId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, HoatDongModelRespuest HoatDongUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var HoatDong = new HoatDongModel
            {
                Content = HoatDongUpdateRequest.Content,
                UpdateDate = dateTime,
                IsCompleted = HoatDongUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = HoatDongUpdateRequest.AppID,
                Months = HoatDongUpdateRequest.Months,
                Years = HoatDongUpdateRequest.Years,
                ClassID = HoatDongUpdateRequest.ClassID,
                KhoaHocId = HoatDongUpdateRequest?.KhoaHocId,
                Img = HoatDongUpdateRequest?.Img,
            };
            var savHoatDongResponse = await _hoatDongService.UpdateHoatDong(id, HoatDong);
            if (!savHoatDongResponse.Success)
            {
                return UnprocessableEntity(savHoatDongResponse);
            }
            var taskResponse = new HoatDongModel
            {
                Id = savHoatDongResponse.HoatDong.Id,
                Content = savHoatDongResponse.HoatDong.Content,
                UpdateDate = dateTime,
                IsCompleted = savHoatDongResponse.HoatDong.IsCompleted,
                Months = savHoatDongResponse.HoatDong.Months,
                Years = savHoatDongResponse?.HoatDong.Years,
                ClassID = savHoatDongResponse?.HoatDong.ClassID,
                KhoaHocId = savHoatDongResponse?.HoatDong.KhoaHocId,
                Img = savHoatDongResponse?.HoatDong.Img
            };

            return Ok(taskResponse);
        }
        [Authorize]
        [HttpGet("bykhoahocclass")]
        public async Task<IActionResult> GetAllFillterByUserKhoaHocClass(string khoaHocId, string classId)
        {
            var getStudentsResponse = await _hoatDongService.GetStudentsFillterByUserKhoaHocClass(UserID_Protected, khoaHocId, classId);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }
            return Ok(getStudentsResponse);
        }
        #region Phu huynh
        [Authorize]
        [HttpGet("ph")]
        public async Task<IActionResult> PH_GetAll()
        {
            var getHoatDongsResponse = await _hoatDongService.PH_GetHoatDongBy(UserID_Protected);
            if (!getHoatDongsResponse.Success)
            {
                return UnprocessableEntity(getHoatDongsResponse);
            }

            var tasksResponse = getHoatDongsResponse.HoatDongs.ConvertAll(o => new HoatDongModel
            {
                Id = o.Id,
                Content = o.Content,
                IsCompleted = o.IsCompleted,
                CreateDate = o.CreateDate,
                UpdateDate = o.UpdateDate,
                AppID = o.AppID,
                ClassID= o.ClassID,
                Months= o.Months,
                Years= o.Years,
                KhoaHocId= o.KhoaHocId,
            });
            return Ok(tasksResponse);
        }
        #endregion
        [Authorize]
        [HttpGet("gv")]
        public async Task<IActionResult> GetByID(int UserID)
        {
            var getHoatDongResponse = await _hoatDongService.GV_GetHoatDongByUserId(UserID);
            if (!getHoatDongResponse.Success)
            {
                return UnprocessableEntity(getHoatDongResponse);
            }
            return Ok(getHoatDongResponse);
        }
        [Authorize]
        [HttpGet("ph/hd")]
        public async Task<IActionResult> PH_GetByHoatDongID(int UserID)
        {
            var getHoatDongResponse = await _hoatDongService.PH_GetHoatDongByUserId(UserID);
            if (!getHoatDongResponse.Success)
            {
                return UnprocessableEntity(getHoatDongResponse);
            }
            return Ok(getHoatDongResponse);
        }
        #region Giáo viên
        //[Authorize]
        //[HttpGet("gv")]
        //public async Task<IActionResult> GV_GetAll()
        //{
        //    var getHoatDongsResponse = await _hoatDongService.GV_GetHoatDongByUser(UserID_Protected);
        //    if (!getHoatDongsResponse.Success)
        //    {
        //        return UnprocessableEntity(getHoatDongsResponse);
        //    }

        //    //var tasksResponse = getHoatDongsResponse.HoatDongModels.ConvertAll(o => new HoatDongModel
        //    //{
        //    //    Id = o.Id,
        //    //    Content = o.Content,
        //    //    IsCompleted = o.IsCompleted,
        //    //    CreateDate = o.CreateDate,
        //    //    UpdateDate = o.UpdateDate,
        //    //    Months = o.Months,
        //    //    Years = o.Years,
        //    //    Img = o.Img,
        //    //    AppID = o.AppID,
        //    //    ClassID = o.ClassID,
        //    //    KhoaHocId = o.KhoaHocId,
        //    //});
        //    return Ok(getHoatDongsResponse);
        //}
        #endregion

        //[Authorize]
        //[HttpPost("uploadfile")]
        //public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
        //{
        //    var result = await _hoatDongService.UploadFile(_IFormFile, UserID_Protected.ToString());
        //    return Ok(result);
        //}

        [Authorize]
        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileStudentModel fileStudentModel)
        {
            var uniqueFileName = Guid.NewGuid().ToString();
            var stringCutted = fileStudentModel.file.FileName.Split('.').Last();
            var fileStudent = new UploadFileStudentModel
            {
                //FileName = fileStudentModel.file.FileName,
                FileName = uniqueFileName + "." + stringCutted,
                file = fileStudentModel.file
            };
            var result = await _hoatDongService.UploadFile(fileStudent, UserID_Protected.ToString());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var result = await _hoatDongService.DownloadFile(fileName);
            return File(result.Item1,result.Item2,result.Item2);
        }
    }
}
