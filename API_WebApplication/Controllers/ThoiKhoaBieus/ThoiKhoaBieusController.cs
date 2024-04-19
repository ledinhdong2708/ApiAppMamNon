using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.Students;
using API_WebApplication.Interfaces.ThoiKhoaBieus;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Requests.ThoiKhoaBieus;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;
using API_WebApplication.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.ThoiKhoaBieus
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiKhoaBieusController : BaseApiController
    {
        private readonly IThoiKhoaBieuService _thoiKhoaBieuService;

        public ThoiKhoaBieusController(IThoiKhoaBieuService thoiKhoaBieuService)
        {
            this._thoiKhoaBieuService = thoiKhoaBieuService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getThoiKhoaBieusResponse = await _thoiKhoaBieuService.GetThoiKhoaBieuByUser(UserID_Protected);
            if (!getThoiKhoaBieusResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieusResponse);
            }

            var tasksResponse = getThoiKhoaBieusResponse.ThoiKhoaBieus.ConvertAll(o => new ThoiKhoaBieuModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                ClassTKB = o.ClassTKB,
                NameTKB = o.NameTKB,
                Command = o.Command,
                Time06300720 = o.Time06300720,
                Time07200730 = o.Time07200730,
                Time07300815 = o.Time07300815,
                Time08150845 = o.Time08150845,
                Time08450900 = o.Time08450900,
                Time09000930 = o.Time09000930,
                Time09301015 = o.Time09301015,
                Time10151115 = o.Time10151115,
                Time11151400 = o.Time11151400,
                Time14001415 = o.Time14001415,
                Time14151500 = o.Time14151500,
                Time15001515 = o.Time15001515,
                Time15151540 = o.Time15151540,
                Time15301630 = o.Time15301630,
                Time16301730 = o.Time16301730,
                Time17301815 = o.Time17301815,
                CreateDate = o.CreateDate,
                KhoaHocId = o.KhoaHocId,
                days = o.days,
                months = o.months,
                years = o.years,
            });
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("bydateclass")]
        public async Task<IActionResult> GetAllFillterByUserDateClass(string day,string month, string year, string classId)
        {
            var getThoiKhoaBieusResponse = await _thoiKhoaBieuService.GetStudentsFillterByUserDateClass(UserID_Protected, day,month,year,classId);
            if (!getThoiKhoaBieusResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieusResponse);
            }
            
            var tasksResponse = getThoiKhoaBieusResponse.ThoiKhoaBieus.ConvertAll(o => new ThoiKhoaBieuModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                ClassTKB = o.ClassTKB,
                NameTKB = o.NameTKB,
                Command = o.Command,
                Time06300720 = o.Time06300720,
                Time07200730 = o.Time07200730,
                Time07300815 = o.Time07300815,
                Time08150845 = o.Time08150845,
                Time08450900 = o.Time08450900,
                Time09000930 = o.Time09000930,
                Time09301015 = o.Time09301015,
                Time10151115 = o.Time10151115,
                Time11151400 = o.Time11151400,
                Time14001415 = o.Time14001415,
                Time14151500 = o.Time14151500,
                Time15001515 = o.Time15001515,
                Time15151540 = o.Time15151540,
                Time15301630 = o.Time15301630,
                Time16301730 = o.Time16301730,
                Time17301815 = o.Time17301815,
                CreateDate = o.CreateDate,
                KhoaHocId = o.KhoaHocId,
                days = o.days,
                months = o.months,
                years = o.years,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getThoiKhoaBieuResponse = await _thoiKhoaBieuService.GetIDThoiKhoaBieu(UserID, StudentID);
            if (!getThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieuResponse);
            }

            var tasksResponse = new ThoiKhoaBieuModel
            {
                Id = getThoiKhoaBieuResponse.ThoiKhoaBieu.Id,
                IsCompleted = getThoiKhoaBieuResponse.ThoiKhoaBieu.IsCompleted,
                ClassTKB = getThoiKhoaBieuResponse.ThoiKhoaBieu.ClassTKB,
                NameTKB = getThoiKhoaBieuResponse.ThoiKhoaBieu.NameTKB,
                Command = getThoiKhoaBieuResponse.ThoiKhoaBieu.Command,
                Time06300720 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time06300720,
                Time07200730 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time07200730,
                Time07300815 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time07300815,
                Time08150845 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time08150845,
                Time08450900 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time08450900,
                Time09000930 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time09000930,
                Time09301015 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time09301015,
                Time10151115 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time10151115,
                Time11151400 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time11151400,
                Time14001415 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time14001415,
                Time14151500 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time14151500,
                Time15001515 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15001515,
                Time15151540 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15151540,
                Time15301630 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15301630,
                Time16301730 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time16301730,
                Time17301815 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time17301815,
                CreateDate = getThoiKhoaBieuResponse.ThoiKhoaBieu.CreateDate,
                KhoaHocId = getThoiKhoaBieuResponse.ThoiKhoaBieu.KhoaHocId,
                days = getThoiKhoaBieuResponse.ThoiKhoaBieu.days,
                months = getThoiKhoaBieuResponse.ThoiKhoaBieu.months,
                years = getThoiKhoaBieuResponse.ThoiKhoaBieu.years,
            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ThoiKhoaBieuRequest thoikhoabieuRequest)
        {
            DateTime dateTime = DateTime.Now;
            var thoikhoabieu = new ThoiKhoaBieuModel
            {
                Time06300720 = thoikhoabieuRequest.Time06300720,
                Time07200730 = thoikhoabieuRequest.Time07200730,
                Time07300815 = thoikhoabieuRequest.Time07300815,
                Time08150845 = thoikhoabieuRequest.Time08150845,
                Time08450900 = thoikhoabieuRequest.Time08450900,
                Time09000930 = thoikhoabieuRequest.Time09000930,
                Time09301015 = thoikhoabieuRequest.Time09301015,
                Time10151115 = thoikhoabieuRequest.Time10151115,
                Time11151400 = thoikhoabieuRequest.Time11151400,
                Time14001415 = thoikhoabieuRequest.Time14001415,
                Time14151500 = thoikhoabieuRequest.Time14151500,
                Time15001515 = thoikhoabieuRequest.Time15001515,
                Time15151540 = thoikhoabieuRequest.Time15151540,
                Time15301630 = thoikhoabieuRequest.Time15301630,
                Time16301730 = thoikhoabieuRequest.Time16301730,
                Time17301815 = thoikhoabieuRequest.Time17301815,
                IsCompleted = thoikhoabieuRequest.IsCompleted,
                CreateDate = dateTime,
                Role = 0,
                ClassTKB = thoikhoabieuRequest.ClassTKB,
                NameTKB = thoikhoabieuRequest.NameTKB,
                Command = thoikhoabieuRequest.Command,
                UserId = UserID_Protected,
                KhoaHocId = thoikhoabieuRequest.KhoaHocId,
                AppID = thoikhoabieuRequest.AppID,
                days = thoikhoabieuRequest.days,
                months = thoikhoabieuRequest.months,
                years = thoikhoabieuRequest.years
            };
            var saveStudentResponse = await _thoiKhoaBieuService.SaveThoiKhoaBieu(thoikhoabieu);
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }
            var taskResponse = new ThoiKhoaBieuModel
            {
                Id = saveStudentResponse.ThoiKhoaBieu.Id,
                IsCompleted = saveStudentResponse.ThoiKhoaBieu.IsCompleted,
                ClassTKB = saveStudentResponse.ThoiKhoaBieu.ClassTKB,
                NameTKB = saveStudentResponse.ThoiKhoaBieu.NameTKB,
                Command = saveStudentResponse.ThoiKhoaBieu.Command,
                Time06300720 = saveStudentResponse.ThoiKhoaBieu.Time06300720,
                Time07200730 = saveStudentResponse.ThoiKhoaBieu.Time07200730,
                Time07300815 = saveStudentResponse.ThoiKhoaBieu.Time07300815,
                Time08150845 = saveStudentResponse.ThoiKhoaBieu.Time08150845,
                Time08450900 = saveStudentResponse.ThoiKhoaBieu.Time08450900,
                Time09000930 = saveStudentResponse.ThoiKhoaBieu.Time09000930,
                Time09301015 = saveStudentResponse.ThoiKhoaBieu.Time09301015,
                Time10151115 = saveStudentResponse.ThoiKhoaBieu.Time10151115,
                Time11151400 = saveStudentResponse.ThoiKhoaBieu.Time11151400,
                Time14001415 = saveStudentResponse.ThoiKhoaBieu.Time14001415,
                Time14151500 = saveStudentResponse.ThoiKhoaBieu.Time14151500,
                Time15001515 = saveStudentResponse.ThoiKhoaBieu.Time15001515,
                Time15151540 = saveStudentResponse.ThoiKhoaBieu.Time15151540,
                Time15301630 = saveStudentResponse.ThoiKhoaBieu.Time15301630,
                Time16301730 = saveStudentResponse.ThoiKhoaBieu.Time16301730,
                Time17301815 = saveStudentResponse.ThoiKhoaBieu.Time17301815,
                CreateDate = saveStudentResponse.ThoiKhoaBieu.CreateDate,
                KhoaHocId = saveStudentResponse.ThoiKhoaBieu.KhoaHocId,
                AppID = saveStudentResponse.ThoiKhoaBieu.AppID,
                days = saveStudentResponse.ThoiKhoaBieu.days,
                months = saveStudentResponse.ThoiKhoaBieu.months,
                years = saveStudentResponse.ThoiKhoaBieu.years
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteThoiKhoaBieuResponse = await _thoiKhoaBieuService.DeleteThoiKhoaBieu(id, UserID_Protected);
            if (!deleteThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(deleteThoiKhoaBieuResponse);
            }
            return Ok(deleteThoiKhoaBieuResponse.ThoiKhoaBieuId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ThoiKhoaBieuRequest thoikhoabieuUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var thoikhoabieu = new ThoiKhoaBieuModel
            {
                IsCompleted = thoikhoabieuUpdateRequest.IsCompleted,
                UpdateDate = dateTime,
                ClassTKB = thoikhoabieuUpdateRequest.ClassTKB,
                NameTKB = thoikhoabieuUpdateRequest.NameTKB,
                Command = thoikhoabieuUpdateRequest.Command,
                Time06300720 = thoikhoabieuUpdateRequest.Time06300720,
                Time07200730 = thoikhoabieuUpdateRequest.Time07200730,
                Time07300815 = thoikhoabieuUpdateRequest.Time07300815,
                Time08150845 = thoikhoabieuUpdateRequest.Time08150845,
                Time08450900 = thoikhoabieuUpdateRequest.Time08450900,
                Time09000930 = thoikhoabieuUpdateRequest.Time09000930,
                Time09301015 = thoikhoabieuUpdateRequest.Time09301015,
                Time10151115 = thoikhoabieuUpdateRequest.Time10151115,
                Time11151400 = thoikhoabieuUpdateRequest.Time11151400,
                Time14001415 = thoikhoabieuUpdateRequest.Time14001415,
                Time14151500 = thoikhoabieuUpdateRequest.Time14151500,
                Time15001515 = thoikhoabieuUpdateRequest.Time15001515,
                Time15151540 = thoikhoabieuUpdateRequest.Time15151540,
                Time15301630 = thoikhoabieuUpdateRequest.Time15301630,
                Time16301730 = thoikhoabieuUpdateRequest.Time16301730,
                Time17301815 = thoikhoabieuUpdateRequest.Time17301815,
                UserId = UserID_Protected,
                KhoaHocId = thoikhoabieuUpdateRequest.KhoaHocId,
                AppID = thoikhoabieuUpdateRequest.AppID,
                days = thoikhoabieuUpdateRequest.days,
                months = thoikhoabieuUpdateRequest.months,
                years = thoikhoabieuUpdateRequest.years
            };
            var saveStudentResponse = await _thoiKhoaBieuService.UpdateThoiKhoaBieu(id, thoikhoabieu);
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }
            var taskResponse = new ThoiKhoaBieuModel
            {
                IsCompleted = saveStudentResponse.ThoiKhoaBieu.IsCompleted,
                ClassTKB = saveStudentResponse.ThoiKhoaBieu.ClassTKB,
                NameTKB = saveStudentResponse.ThoiKhoaBieu.NameTKB,
                Command = saveStudentResponse.ThoiKhoaBieu.Command,
                Time06300720 = saveStudentResponse.ThoiKhoaBieu.Time06300720,
                Time07200730 = saveStudentResponse.ThoiKhoaBieu.Time07200730,
                Time07300815 = saveStudentResponse.ThoiKhoaBieu.Time07300815,
                Time08150845 = saveStudentResponse.ThoiKhoaBieu.Time08150845,
                Time08450900 = saveStudentResponse.ThoiKhoaBieu.Time08450900,
                Time09000930 = saveStudentResponse.ThoiKhoaBieu.Time09000930,
                Time09301015 = saveStudentResponse.ThoiKhoaBieu.Time09301015,
                Time10151115 = saveStudentResponse.ThoiKhoaBieu.Time10151115,
                Time11151400 = saveStudentResponse.ThoiKhoaBieu.Time11151400,
                Time14001415 = saveStudentResponse.ThoiKhoaBieu.Time14001415,
                Time14151500 = saveStudentResponse.ThoiKhoaBieu.Time14151500,
                Time15001515 = saveStudentResponse.ThoiKhoaBieu.Time15001515,
                Time15151540 = saveStudentResponse.ThoiKhoaBieu.Time15151540,
                Time15301630 = saveStudentResponse.ThoiKhoaBieu.Time15301630,
                Time16301730 = saveStudentResponse.ThoiKhoaBieu.Time16301730,
                Time17301815 = saveStudentResponse.ThoiKhoaBieu.Time17301815,
                UpdateDate = dateTime,
                KhoaHocId = saveStudentResponse.ThoiKhoaBieu.KhoaHocId,
                days = saveStudentResponse.ThoiKhoaBieu.days,
                months = saveStudentResponse.ThoiKhoaBieu.months,
                years = saveStudentResponse.ThoiKhoaBieu.years
            };

            return Ok(taskResponse);
        }
        #region Phụ huynh
        [Authorize]
        [HttpGet("ph/byId")]
        public async Task<IActionResult> PH_GetByIDStudent(string day, string month, string year,int StudentID)
        {
            var getThoiKhoaBieuResponse = await _thoiKhoaBieuService.PH_GetIDByStudent(day, month,year,StudentID, UserID_Protected);
            if (!getThoiKhoaBieuResponse.Success)
            {
                return UnprocessableEntity(getThoiKhoaBieuResponse);
            }

            var tasksResponse = new ThoiKhoaBieuModel
            {
                Id = getThoiKhoaBieuResponse.ThoiKhoaBieu.Id,
                IsCompleted = getThoiKhoaBieuResponse.ThoiKhoaBieu.IsCompleted,
                ClassTKB = getThoiKhoaBieuResponse.ThoiKhoaBieu.ClassTKB,
                NameTKB = getThoiKhoaBieuResponse.ThoiKhoaBieu.NameTKB,
                Command = getThoiKhoaBieuResponse.ThoiKhoaBieu.Command,
                Time06300720 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time06300720,
                Time07200730 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time07200730,
                Time07300815 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time07300815,
                Time08150845 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time08150845,
                Time08450900 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time08450900,
                Time09000930 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time09000930,
                Time09301015 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time09301015,
                Time10151115 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time10151115,
                Time11151400 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time11151400,
                Time14001415 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time14001415,
                Time14151500 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time14151500,
                Time15001515 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15001515,
                Time15151540 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15151540,
                Time15301630 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time15301630,
                Time16301730 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time16301730,
                Time17301815 = getThoiKhoaBieuResponse.ThoiKhoaBieu.Time17301815,
                CreateDate = getThoiKhoaBieuResponse.ThoiKhoaBieu.CreateDate,
                KhoaHocId = getThoiKhoaBieuResponse.ThoiKhoaBieu.KhoaHocId,
                days = getThoiKhoaBieuResponse.ThoiKhoaBieu.days,
                months = getThoiKhoaBieuResponse.ThoiKhoaBieu.months,
                years = getThoiKhoaBieuResponse.ThoiKhoaBieu.years
            };
            return Ok(tasksResponse);
        }
        #endregion
    }
}
