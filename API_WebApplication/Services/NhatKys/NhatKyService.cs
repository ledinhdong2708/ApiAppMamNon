using API_WebApplication.DTO.NhatKy;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Interfaces.NhatKy;
using API_WebApplication.Models;
using API_WebApplication.Requests.NhatKy;
using API_WebApplication.Requests.Students;
using API_WebApplication.Requests.TableLikeRequest;
using API_WebApplication.Responses.NhatKys;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.XinNghiPhep;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_WebApplication.Services.NhatKys
{
    public class NhatKyService : INhatKyService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly IMapper _mapper;
        public NhatKyService(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            this._mapper = mapper;
        }

        public async Task<DeleteNhatKyResponse> DeleteNhatKy(int NhatKyId, int userId)
        {
            var nhatKy = await _aPI_Application_V1Context.NhatKies.FindAsync(NhatKyId);
            if (nhatKy == null)
            {
                return new DeleteNhatKyResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (nhatKy.UserId != userId)
            {
                return new DeleteNhatKyResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.NhatKies.Remove(nhatKy);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteNhatKyResponse
                {
                    Success = true,
                    NhatKyId = nhatKy.Id
                };
            }
            return new DeleteNhatKyResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<NhatKyResponse> GetIDNhatKy(int userId, int NhatKyId)
        {
            var nhatKy = await _aPI_Application_V1Context.NhatKies
                .Include(_ => _.BinhLuans).Include(_ => _.TableLikes).Include(_=>_.TableImages).Include(_=>_.BinhLuans)
                .Where(_ => _.Id == NhatKyId)
                .FirstOrDefaultAsync();
            if (nhatKy == null)
            {
                return new NhatKyResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (nhatKy.UserId != userId)
            {
                return new NhatKyResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (nhatKy.Id < 0)
            {
                return new NhatKyResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new NhatKyResponse
            {
                Success = true,
                Data = nhatKy
            };
        }

        public async Task<GetNhatKyGetAllResponse> GetNhatKysByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var nhatKy = await _aPI_Application_V1Context.NhatKies
                .Where(_ => _.AppID == appID!.AppID)
                .Include(_ => _.BinhLuans)
                .Include(_ => _.TableLikes)
                .Include(_ => _.TableImages)
                .Select(o => new NhatKyGetAll
                {
                    AppID = o.AppID,
                    BinhLuanId = o.BinhLuanId,
                    BinhLuans = o.BinhLuans,
                    ClassId = o.ClassId,
                    Content = o.Content,
                    CreateDate = o.CreateDate,
                    Id = o.Id,
                    KhoaId = o.KhoaId,
                    Status = o.Status,
                    StudentId = o.StudentId,
                    NameStudent = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.NameStudent,
                    TableImageId = o.TableImageId,
                    TableImages = o.TableImages,
                    TableLikeId = o.TableLikeId,
                    TableLikes = _aPI_Application_V1Context.TableLikes.Where(_ => _.NhatKyId.ToString() == o.Id.ToString() && _.UserId == userId).Select(a => new TableLike
                    {
                        Id = a.Id,
                        Content = a.Content,
                        CreateDate = a.CreateDate,
                        Status = a.Status,
                        NhatKyId = a.NhatKyId,
                        UserId = a.UserId,
                        StudentId = a.StudentId,
                        AppID = a.AppID,
                    }).ToList(),
                    UpdateDate = o.UpdateDate,
                    UserId = o.UserId,
                    AvatarPatch = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.imagePatch,
                    imgString = _aPI_Application_V1Context.TableImages.Where(_ => _.NhatKyId.ToString() == o.Id.ToString()).Select(a => a.ImagePatch).ToList()!,
                }).OrderByDescending(c => c.CreateDate)
                .ToListAsync();
            if (nhatKy.Count == 0)
            {
                List<NhatKyGetAll> value = new List<NhatKyGetAll>();
                return new GetNhatKyGetAllResponse
                {
                    //Success = false,
                    //Error = "No Student found for this user",
                    //ErrorCode = "T04",
                    //Data = nhatKy.ToList()
                    Success = true,
                    NhatKys = value
                };
            }
            return new GetNhatKyGetAllResponse { Success = true, NhatKys = nhatKy };
        }

        public async Task<List<string>> getArrayImage(string id)
        {
            var result = await _aPI_Application_V1Context.TableImages.Where(_ => _.NhatKyId.ToString() == id).ToListAsync();
            List<string> data = new List<string>();
            foreach (var item in result)
            {
                data.Add(item.ImagePatch.ToString());
            }
            return data;
        }

        public async Task<NhatKyResponse> PH_GetIDNhatKyByStudent(int id)
        {
            var studentId = await _aPI_Application_V1Context.Students.Where(o => o.Id == id && o.IsCompleted == false).FirstOrDefaultAsync();
            var nhatKy = await _aPI_Application_V1Context.NhatKies
                    .Where(_ => _.StudentId == studentId.Id.ToString())
                    .Include(_ => _.BinhLuans)
                    .Include(_ => _.TableLikes)
                    .Include(_ => _.TableImages)
                    .FirstOrDefaultAsync();
            if (nhatKy == null)
            {
                return new NhatKyResponse
                {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (nhatKy.Id < 0)
            {
                return new NhatKyResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new NhatKyResponse
            {
                Success = true,
                Data = nhatKy
            };
        }

        public async Task<SaveNhatKyResponse> SaveNhatKy(NhatKyDTO NhatKy)
        {
            var newNhatKy = _mapper.Map<NhatKy>(NhatKy);
            await _aPI_Application_V1Context.AddAsync(newNhatKy);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveNhatKyResponse
                {
                    Success = true,
                    Data = newNhatKy
                };
            }
            return new SaveNhatKyResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateNhatKyResponse> UpdateNhatKy(int NhatKyId, NhatKyDTO NhatKy)
        {
            var updateNhatKy = _mapper.Map<NhatKy>(NhatKy);
            _aPI_Application_V1Context.Update(updateNhatKy);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateNhatKyResponse
                {
                    Success = true,
                    NhatKy = updateNhatKy
                };
            }
            return new UpdateNhatKyResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        //public async Task<NhatKyResponse> AddMultipleImageNhatKy([FromForm] UploadMultipleFileImageNhatKy fileNhatKyModel, string idUser)
        public async Task<List<UploadFileNhatKyRequest>> AddMultipleImageNhatKy(List<IFormFile> fileNhatKyModel, string idUser)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id.ToString() == idUser).FirstOrDefaultAsync();
            List<UploadFileNhatKyRequest> _result = new List<UploadFileNhatKyRequest>();
            DateTime dateTime = DateTime.Now;
            //string root = @"C:\imageAPIApp\" + idUser;
            string root = @"upload\nhatky" + idUser;
            // If directory does not exist, create it.
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            foreach (var file in fileNhatKyModel)
            {
                string patch = Path.Combine(root, file.FileName);
                var uniqueFileName = Guid.NewGuid().ToString();
                var stringCutted = file.FileName.Split('.').Last();

                var fileDetail = new TableImage()
                {
                    Id = 0,
                    ImageName = uniqueFileName + ".jpg",
                    ImagePatch = Path.Combine(root, (uniqueFileName + ".jpg")),
                    NhatKyId = 0,
                    UserId = int.Parse(idUser),
                    CreateDate = dateTime
                };
                using (Stream stream = new FileStream(fileDetail.ImagePatch, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    //stream.Close();
                }
                _result.Add(new UploadFileNhatKyRequest
                {
                    Id = _result.Count + 1,
                    CreateDate = dateTime,
                    UserId = idUser,
                    ImageName = fileDetail.ImageName,
                    ImagePatch = fileDetail.ImagePatch,
                    AppID = appID!.AppID.ToString(),
                });
            }

            return _result;
        }

        public async Task<GetNhatKyGetAllResponse> PH_GetNhatKysByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var studentID = await _aPI_Application_V1Context.Students.Where(o => o.Id == appID!.StudentId).FirstOrDefaultAsync();
            var nhatKy = await _aPI_Application_V1Context.NhatKies
                //.Where(_ => _.StudentId == appID!.StudentId.ToString())
                .Include(_ => _.BinhLuans)
                .Include(_ => _.TableLikes)
                .Include(_ => _.TableImages)
                .Select(o => new NhatKyGetAll
                {
                    AppID = o.AppID,
                    BinhLuanId = o.BinhLuanId,
                    BinhLuans = o.BinhLuans,
                    ClassId = o.ClassId,
                    Content = o.Content,
                    CreateDate = o.CreateDate,
                    Id = o.Id,
                    KhoaId = o.KhoaId,
                    Status = o.Status,
                    StudentId = o.StudentId,
                    NameStudent = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.NameStudent,
                    TableImageId = o.TableImageId,
                    TableImages = o.TableImages,
                    TableLikeId = o.TableLikeId,
                    TableLikes = _aPI_Application_V1Context.TableLikes.Where(_ => _.NhatKyId.ToString() == o.Id.ToString() && _.UserId == userId).Select(a => new TableLike
                    {
                        Id = a.Id,
                        Content = a.Content,
                        CreateDate = a.CreateDate,
                        Status = a.Status,
                        NhatKyId = a.NhatKyId,
                        UserId = a.UserId,
                        StudentId = a.StudentId,
                        AppID = a.AppID,
                    }).ToList(),
                    UpdateDate = o.UpdateDate,
                    UserId = o.UserId,
                    AvatarPatch = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.imagePatch,
                    imgString = _aPI_Application_V1Context.TableImages.Where(_ => _.NhatKyId.ToString() == o.Id.ToString()).Select(a => a.ImagePatch).ToList()!,
                    ClassID = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.Class1,
                    KhoaHocID = _aPI_Application_V1Context.Students.Where(_ => _.Id.ToString() == o.StudentId).FirstOrDefault()!.Year1
                }).Where(c => c.ClassID == studentID!.Class1 && c.KhoaHocID == studentID!.Year1)
                .OrderByDescending(c => c.CreateDate)
                .ToListAsync();
            if (nhatKy.Count == 0)
            {
                List<NhatKyGetAll> value = new List<NhatKyGetAll>();
                return new GetNhatKyGetAllResponse
                {
                    //Success = false,
                    //Error = "No Student found for this user",
                    //ErrorCode = "T04",
                    //Data = nhatKy.ToList()
                    Success = true,
                    NhatKys = value
                };
            }
            return new GetNhatKyGetAllResponse { Success = true, NhatKys = nhatKy };
        }

        public async Task<List<UploadFileNhatKyRequest>> UploadFileMultipleNhatKy(List<IFormFile> fileNhatKyModel, string idUser)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id.ToString() == idUser).FirstOrDefaultAsync();
            List<UploadFileNhatKyRequest> _result = new List<UploadFileNhatKyRequest>();
            DateTime dateTime = DateTime.Now;
            //string root = @"C:\imageAPIApp\" + idUser;
            string root = @"upload\nhatky" + idUser;
            // If directory does not exist, create it.
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            foreach (var file in fileNhatKyModel)
            {
                string patch = Path.Combine(root, file.FileName);
                var uniqueFileName = Guid.NewGuid().ToString();
                var stringCutted = file.FileName.Split('.').Last();

                var fileDetail = new TableImage()
                {
                    Id = 0,
                    ImageName = uniqueFileName +"."+ stringCutted,
                    ImagePatch = Path.Combine(root, (uniqueFileName +"."+ stringCutted)),
                    NhatKyId = 0,
                    UserId = int.Parse(idUser),
                    CreateDate = dateTime
                };
                using (Stream stream = new FileStream(fileDetail.ImagePatch, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    //stream.Close();
                }
                _result.Add(new UploadFileNhatKyRequest
                {
                    Id = _result.Count + 1,
                    CreateDate = dateTime,
                    UserId = idUser,
                    ImageName = fileDetail.ImageName,
                    ImagePatch = fileDetail.ImagePatch,
                    AppID = appID!.AppID.ToString(),
                });
            }

            return _result;
        }
    }
}
