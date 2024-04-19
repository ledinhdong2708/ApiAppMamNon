using API_WebApplication.Controllers.Bases;
using API_WebApplication.DTO.NhatKy;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Interfaces.NhatKy;
using API_WebApplication.Interfaces.XinNghiPheps;
using API_WebApplication.Models;
using API_WebApplication.Requests.NhatKy;
using API_WebApplication.Requests.Students;
using API_WebApplication.Services.XinNghiPheps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Controllers.NhatKys
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhatKyController : BaseApiController
    {
        private readonly INhatKyService _nhatKyService;
        public NhatKyController(INhatKyService nhatKyService)
        {
            _nhatKyService = nhatKyService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getNhatKyResponse = await _nhatKyService.GetNhatKysByUser(UserID_Protected);
            if (!getNhatKyResponse.Success)
            {
                return UnprocessableEntity(getNhatKyResponse);
            }


            //var result = getNhatKyResponse.NhatKys.ConvertAll(o => new NhatKyGetAll
            //{
            //    AppID = o.AppID,
            //    BinhLuanId = o.BinhLuanId,
            //    BinhLuans = o.BinhLuans,
            //    ClassId = o.ClassId,
            //    Content = o.Content,
            //    CreateDate = o.CreateDate,
            //    Id = o.Id,
            //    KhoaId = o.KhoaId,
            //    Status = o.Status,
            //    StudentId = o.StudentId,
            //    TableImageId = o.TableImageId,
            //    TableImages = o.TableImages,
            //    TableLikeId = o.TableLikeId,
            //    TableLikes = o.TableLikes,
            //    UpdateDate = o.UpdateDate,
            //    UserId = o.UserId,
            //    Student = API_Application_V1Contex
            //});

            return Ok(getNhatKyResponse);
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int UserID, int XinNghiPhepID)
        {
            var getNhatKyResponse = await _nhatKyService.GetIDNhatKy(UserID, XinNghiPhepID);
            if (!getNhatKyResponse.Success)
            {
                return UnprocessableEntity(getNhatKyResponse);
            }
            return Ok(getNhatKyResponse);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(NhatKyDTO _nhatKy)
        {
            _nhatKy.UserId = UserID_Protected;
            _nhatKy.CreateDate = DateTime.Now;
            var getNhatKyResponse = await _nhatKyService.SaveNhatKy(_nhatKy);
            if (!getNhatKyResponse.Success)
            {
                return UnprocessableEntity(getNhatKyResponse);
            }

            return Ok(getNhatKyResponse);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, NhatKyDTO _nhatKy)
        {
            var updateXinNghiPhepResponse = await _nhatKyService.UpdateNhatKy(id, _nhatKy);
            if (!updateXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(updateXinNghiPhepResponse);
            }
            return Ok(updateXinNghiPhepResponse);
        }

        [Authorize]
        [HttpGet("ph")]
        public async Task<IActionResult> PH_GetIDByHocPhiStudent(int id)
        {
            var getXinNghiPhepResponse = await _nhatKyService.PH_GetIDNhatKyByStudent(id);
            if (!getXinNghiPhepResponse.Success)
            {
                return UnprocessableEntity(getXinNghiPhepResponse);
            }

            return Ok(getXinNghiPhepResponse);
        }

        [Authorize]
        [HttpPost("UploadMultipleFile")]
        //[RequestSizeLimit(1000 * 1024 * 1024 )]
        //public async Task<IActionResult> UploadMultipleImageNhatKy(UploadMultipleFileImageNhatKy fileNhatKyModel)
        public async Task<List<UploadFileNhatKyRequest>> UploadMultipleImageNhatKy(List<IFormFile> fileNhatKyModel)
        {
            //var uniqueFileName = Guid.NewGuid().ToString();
            //var stringCutted = fileStudentModel.file.FileName.Split('.').Last();
            //var fileStudent = new UploadFileStudentModel
            //{
            //    //FileName = fileStudentModel.file.FileName,
            //    FileName = uniqueFileName + "." + stringCutted,
            //    file = fileStudentModel.file
            //};
            var saveStudentResponse = await _nhatKyService.AddMultipleImageNhatKy(fileNhatKyModel, UserID_Protected.ToString());
            //if (!saveStudentResponse.Success)
            //{
            //    return UnprocessableEntity(saveStudentResponse);
            //}

            return saveStudentResponse;

        }

        [Authorize]
        [HttpPost("UploadMultipleFileVideo")]
        //[RequestSizeLimit(1000 * 1024 * 1024 )]
        //public async Task<IActionResult> UploadMultipleImageNhatKy(UploadMultipleFileImageNhatKy fileNhatKyModel)
        public async Task<List<UploadFileNhatKyRequest>> UploadMultipleVideoNhatKy(List<IFormFile> fileNhatKyModel)
        {
            //var uniqueFileName = Guid.NewGuid().ToString();
            //var stringCutted = fileStudentModel.file.FileName.Split('.').Last();
            //var fileStudent = new UploadFileStudentModel
            //{
            //    //FileName = fileStudentModel.file.FileName,
            //    FileName = uniqueFileName + "." + stringCutted,
            //    file = fileStudentModel.file
            //};
            var saveStudentResponse = await _nhatKyService.UploadFileMultipleNhatKy(fileNhatKyModel, UserID_Protected.ToString());
            //if (!saveStudentResponse.Success)
            //{
            //    return UnprocessableEntity(saveStudentResponse);
            //}

            return saveStudentResponse;

        }

        [Authorize]
        [HttpGet("ph/nhatky")]
        public async Task<IActionResult> PH_GetAll()
        {
            var getNhatKyResponse = await _nhatKyService.PH_GetNhatKysByUser(UserID_Protected);
            if (!getNhatKyResponse.Success)
            {
                return UnprocessableEntity(getNhatKyResponse);
            }


            //var result = getNhatKyResponse.NhatKys.ConvertAll(o => new NhatKyGetAll
            //{
            //    AppID = o.AppID,
            //    BinhLuanId = o.BinhLuanId,
            //    BinhLuans = o.BinhLuans,
            //    ClassId = o.ClassId,
            //    Content = o.Content,
            //    CreateDate = o.CreateDate,
            //    Id = o.Id,
            //    KhoaId = o.KhoaId,
            //    Status = o.Status,
            //    StudentId = o.StudentId,
            //    TableImageId = o.TableImageId,
            //    TableImages = o.TableImages,
            //    TableLikeId = o.TableLikeId,
            //    TableLikes = o.TableLikes,
            //    UpdateDate = o.UpdateDate,
            //    UserId = o.UserId,
            //    Student = API_Application_V1Contex
            //});

            return Ok(getNhatKyResponse);
        }
    }
}
