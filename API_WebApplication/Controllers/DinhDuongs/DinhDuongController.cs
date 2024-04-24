using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.ThoiKhoaBieus;
using API_WebApplication.Models;
using API_WebApplication.Requests.DinhDuong;
using API_WebApplication.Requests.ThoiKhoaBieus;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;
using API_WebApplication.Services.ThoiKhoaBieus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace API_WebApplication.Controllers.DinhDuongs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DinhDuongController : BaseApiController
    {
        private readonly IDinhDuongService _dinhduongService;

        public DinhDuongController(IDinhDuongService dinhduongService)
        {
            this._dinhduongService = dinhduongService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getDinhDuongsResponse = await _dinhduongService.GetDinhDuongByUser(UserID_Protected);
            if (!getDinhDuongsResponse.Success)
            {
                return UnprocessableEntity(getDinhDuongsResponse);
            }

            var tasksResponse = getDinhDuongsResponse.DinhDuongs.ConvertAll(o => new DinhDuongModel
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                DocDate = o.DocDate,
                BuoiSang = o.BuoiSang,
                BuoiTrua = o.BuoiTrua,
                BuoiChinhChieu = o.BuoiChinhChieu,
                BuoiPhuChieu = o.BuoiPhuChieu,
                Dam = o.Dam,
                DamDinhMuc = o.DamDinhMuc,
                Beo = o.Beo,
                BeoDinhMuc = o.BeoDinhMuc,
                Duong = o.Duong,
                DuongDinhMuc = o.DuongDinhMuc,
                NangLuong = o.NangLuong,
                NangLuongDinhMuc = o.NangLuongDinhMuc,
                CreateDate = o.CreateDate,
                KhoaHocID = o.KhoaHocID,
                ClassID = o.ClassID,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int UserID, int StudentID)
        {
            var getDinhDuongResponse = await _dinhduongService.GetIDDinhDuong(UserID, StudentID);
            if (!getDinhDuongResponse.Success)
            {
                return UnprocessableEntity(getDinhDuongResponse);
            }

            var tasksResponse = new DinhDuongModel
            {
                Id = getDinhDuongResponse.DinhDuong.Id,
                DocDate = getDinhDuongResponse.DinhDuong.DocDate,
                BuoiSang = getDinhDuongResponse.DinhDuong.BuoiSang,
                BuoiTrua = getDinhDuongResponse.DinhDuong.BuoiTrua,
                BuoiChinhChieu = getDinhDuongResponse.DinhDuong.BuoiChinhChieu,
                BuoiPhuChieu = getDinhDuongResponse.DinhDuong.BuoiPhuChieu,
                Dam = getDinhDuongResponse.DinhDuong.Dam,
                DamDinhMuc = getDinhDuongResponse.DinhDuong.DamDinhMuc,
                Beo = getDinhDuongResponse.DinhDuong.Beo,
                BeoDinhMuc = getDinhDuongResponse.DinhDuong.BeoDinhMuc,
                Duong = getDinhDuongResponse.DinhDuong.Duong,
                DuongDinhMuc = getDinhDuongResponse.DinhDuong.DuongDinhMuc,
                NangLuong = getDinhDuongResponse.DinhDuong.NangLuong,
                NangLuongDinhMuc = getDinhDuongResponse.DinhDuong.NangLuongDinhMuc,
                CreateDate = getDinhDuongResponse.DinhDuong.CreateDate,
                IsCompleted = getDinhDuongResponse.DinhDuong.IsCompleted,
                KhoaHocID = getDinhDuongResponse.DinhDuong.KhoaHocID,
                ClassID = getDinhDuongResponse.DinhDuong.ClassID
            
            };
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(DinhDuongRequest dinhduongRequest)
        {
            DateTime dateTime = DateTime.Now;
            var dinhduong = new DinhDuongModel
            {
                DocDate = dinhduongRequest.DocDate,
                BuoiSang = dinhduongRequest.BuoiSang,
                BuoiTrua = dinhduongRequest.BuoiTrua,
                BuoiChinhChieu = dinhduongRequest.BuoiChinhChieu,
                BuoiPhuChieu = dinhduongRequest.BuoiPhuChieu,
                Dam = dinhduongRequest.Dam,
                DamDinhMuc = dinhduongRequest.DamDinhMuc,
                Beo = dinhduongRequest.Beo,
                BeoDinhMuc = dinhduongRequest.BeoDinhMuc,
                Duong = dinhduongRequest.Duong,
                DuongDinhMuc = dinhduongRequest.DuongDinhMuc,
                NangLuong = dinhduongRequest.NangLuong,
                NangLuongDinhMuc = dinhduongRequest.NangLuongDinhMuc,
                CreateDate = dateTime,
                Role = 0,
                IsCompleted = dinhduongRequest.IsCompleted,
                AppID = dinhduongRequest.AppID,
                UserId = UserID_Protected,
                KhoaHocID = dinhduongRequest.KhoaHocID,
                ClassID = dinhduongRequest.ClassID
            };
            var saveDinhDuongResponse = await _dinhduongService.SaveDinhDuong(dinhduong);
            if (!saveDinhDuongResponse.Success)
            {
                return UnprocessableEntity(saveDinhDuongResponse);
            }
            var taskResponse = new DinhDuongModel
            {
                Id = saveDinhDuongResponse.DinhDuong.Id,
                DocDate = saveDinhDuongResponse.DinhDuong.DocDate,
                BuoiSang = saveDinhDuongResponse.DinhDuong.BuoiSang,
                BuoiTrua = saveDinhDuongResponse.DinhDuong.BuoiTrua,
                BuoiChinhChieu = saveDinhDuongResponse.DinhDuong.BuoiChinhChieu,
                BuoiPhuChieu = saveDinhDuongResponse.DinhDuong.BuoiPhuChieu,
                Dam = saveDinhDuongResponse.DinhDuong.Dam,
                DamDinhMuc = saveDinhDuongResponse.DinhDuong.DamDinhMuc,
                Beo = saveDinhDuongResponse.DinhDuong.Beo,
                BeoDinhMuc = saveDinhDuongResponse.DinhDuong.BeoDinhMuc,
                Duong = saveDinhDuongResponse.DinhDuong.Duong,
                DuongDinhMuc = saveDinhDuongResponse.DinhDuong.DuongDinhMuc,
                NangLuong = saveDinhDuongResponse.DinhDuong.NangLuong,
                NangLuongDinhMuc = saveDinhDuongResponse.DinhDuong.NangLuongDinhMuc,
                CreateDate = saveDinhDuongResponse.DinhDuong.CreateDate,
                IsCompleted = saveDinhDuongResponse.DinhDuong.IsCompleted,
                KhoaHocID = saveDinhDuongResponse.DinhDuong.KhoaHocID,
                ClassID = saveDinhDuongResponse.DinhDuong.ClassID
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteDinhDuongResponse = await _dinhduongService.DeleteDinhDuong(id, UserID_Protected);
            if (!deleteDinhDuongResponse.Success)
            {
                return UnprocessableEntity(deleteDinhDuongResponse);
            }
            return Ok(deleteDinhDuongResponse.DinhDuongId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DinhDuongRequest dinhduongUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var dinhduong = new DinhDuongModel
            {
                DocDate = dinhduongUpdateRequest.DocDate,
                BuoiSang = dinhduongUpdateRequest.BuoiSang,
                BuoiTrua = dinhduongUpdateRequest.BuoiTrua,
                BuoiChinhChieu = dinhduongUpdateRequest.BuoiChinhChieu,
                BuoiPhuChieu = dinhduongUpdateRequest.BuoiPhuChieu,
                Dam = dinhduongUpdateRequest.Dam,
                DamDinhMuc = dinhduongUpdateRequest.DamDinhMuc,
                Beo = dinhduongUpdateRequest.Beo,
                BeoDinhMuc = dinhduongUpdateRequest.BeoDinhMuc,
                Duong = dinhduongUpdateRequest.Duong,
                DuongDinhMuc = dinhduongUpdateRequest.DuongDinhMuc,
                NangLuong = dinhduongUpdateRequest.NangLuong,
                NangLuongDinhMuc = dinhduongUpdateRequest.NangLuongDinhMuc,
                UpdateDate = dateTime,
                IsCompleted = dinhduongUpdateRequest.IsCompleted,
                UserId = UserID_Protected,
                AppID = dinhduongUpdateRequest.AppID,
                KhoaHocID = dinhduongUpdateRequest.KhoaHocID,
                ClassID = dinhduongUpdateRequest.ClassID
            };
            var saveDinhDuongResponse = await _dinhduongService.UpdateDinhDuong(id, dinhduong);
            if (!saveDinhDuongResponse.Success)
            {
                return UnprocessableEntity(saveDinhDuongResponse);
            }
            var taskResponse = new DinhDuongModel
            {
                Id = saveDinhDuongResponse.DinhDuong.Id,
                DocDate = saveDinhDuongResponse.DinhDuong.DocDate,
                BuoiSang = saveDinhDuongResponse.DinhDuong.BuoiSang,
                BuoiTrua = saveDinhDuongResponse.DinhDuong.BuoiTrua,
                BuoiChinhChieu = saveDinhDuongResponse.DinhDuong.BuoiChinhChieu,
                BuoiPhuChieu = saveDinhDuongResponse.DinhDuong.BuoiPhuChieu,
                Dam = saveDinhDuongResponse.DinhDuong.Dam,
                DamDinhMuc = saveDinhDuongResponse.DinhDuong.DamDinhMuc,
                Beo = saveDinhDuongResponse.DinhDuong.Beo,
                BeoDinhMuc = saveDinhDuongResponse.DinhDuong.BeoDinhMuc,
                Duong = saveDinhDuongResponse.DinhDuong.Duong,
                DuongDinhMuc = saveDinhDuongResponse.DinhDuong.DuongDinhMuc,
                NangLuong = saveDinhDuongResponse.DinhDuong.NangLuong,
                NangLuongDinhMuc = saveDinhDuongResponse.DinhDuong.NangLuongDinhMuc,
                UpdateDate = dateTime,
                IsCompleted = saveDinhDuongResponse.DinhDuong.IsCompleted,
                AppID =  saveDinhDuongResponse.DinhDuong.AppID,
                KhoaHocID = saveDinhDuongResponse.DinhDuong.KhoaHocID,
                ClassID = saveDinhDuongResponse.DinhDuong.ClassID
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("date")]
        public async Task<IActionResult> GetIDDinhDuongByDate(int UserID, int day, int month, int year, string khoahocid, string classid)
        {
            var getDinhDuongResponse = await _dinhduongService.GetIDDinhDuongByDate(UserID, day,month,year, khoahocid, classid);
            if (!getDinhDuongResponse.Success)
            {
                return UnprocessableEntity(getDinhDuongResponse);
            }

            var tasksResponse = new DinhDuongModel
            {
                Id = getDinhDuongResponse.DinhDuong.Id,
                DocDate = getDinhDuongResponse.DinhDuong.DocDate,
                BuoiSang = getDinhDuongResponse.DinhDuong.BuoiSang,
                BuoiTrua = getDinhDuongResponse.DinhDuong.BuoiTrua,
                BuoiChinhChieu = getDinhDuongResponse.DinhDuong.BuoiChinhChieu,
                BuoiPhuChieu = getDinhDuongResponse.DinhDuong.BuoiPhuChieu,
                Dam = getDinhDuongResponse.DinhDuong.Dam,
                DamDinhMuc = getDinhDuongResponse.DinhDuong.DamDinhMuc,
                Beo = getDinhDuongResponse.DinhDuong.Beo,
                BeoDinhMuc = getDinhDuongResponse.DinhDuong.BeoDinhMuc,
                Duong = getDinhDuongResponse.DinhDuong.Duong,
                DuongDinhMuc = getDinhDuongResponse.DinhDuong.DuongDinhMuc,
                NangLuong = getDinhDuongResponse.DinhDuong.NangLuong,
                NangLuongDinhMuc = getDinhDuongResponse.DinhDuong.NangLuongDinhMuc,
                CreateDate = getDinhDuongResponse.DinhDuong.CreateDate,
                IsCompleted = getDinhDuongResponse.DinhDuong.IsCompleted,
                KhoaHocID = getDinhDuongResponse.DinhDuong.KhoaHocID,
                ClassID = getDinhDuongResponse.DinhDuong.ClassID,

            };
            return Ok(tasksResponse);
        }
        [Authorize]
        [HttpGet("ph/date")]
        public async Task<IActionResult> PH_GetByStudent(int day, int month, int year)
        {
            var getDinhDuongResponse = await _dinhduongService.PH_GetIDDinhDuongByDate(day, month, year, UserID_Protected);
            if (!getDinhDuongResponse.Success)
            {
                return UnprocessableEntity(getDinhDuongResponse);
            }

            var tasksResponse = new DinhDuongModel
            {
                Id = getDinhDuongResponse.DinhDuong.Id,
                DocDate = getDinhDuongResponse.DinhDuong.DocDate,
                BuoiSang = getDinhDuongResponse.DinhDuong.BuoiSang,
                BuoiTrua = getDinhDuongResponse.DinhDuong.BuoiTrua,
                BuoiChinhChieu = getDinhDuongResponse.DinhDuong.BuoiChinhChieu,
                BuoiPhuChieu = getDinhDuongResponse.DinhDuong.BuoiPhuChieu,
                Dam = getDinhDuongResponse.DinhDuong.Dam,
                DamDinhMuc = getDinhDuongResponse.DinhDuong.DamDinhMuc,
                Beo = getDinhDuongResponse.DinhDuong.Beo,
                BeoDinhMuc = getDinhDuongResponse.DinhDuong.BeoDinhMuc,
                Duong = getDinhDuongResponse.DinhDuong.Duong,
                DuongDinhMuc = getDinhDuongResponse.DinhDuong.DuongDinhMuc,
                NangLuong = getDinhDuongResponse.DinhDuong.NangLuong,
                NangLuongDinhMuc = getDinhDuongResponse.DinhDuong.NangLuongDinhMuc,
                CreateDate = getDinhDuongResponse.DinhDuong.CreateDate,
                IsCompleted = getDinhDuongResponse.DinhDuong.IsCompleted,
                KhoaHocID = getDinhDuongResponse.DinhDuong.KhoaHocID,
                ClassID = getDinhDuongResponse.DinhDuong.ClassID
            };
            return Ok(tasksResponse);
        }
    }
}
