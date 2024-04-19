
using API_WebApplication.DTO.NhatKy;
using API_WebApplication.Models;
using API_WebApplication.Requests.NhatKy;
using API_WebApplication.Responses.NhatKys;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Interfaces.NhatKy
{
    public interface INhatKyService
    {
        Task<GetNhatKyGetAllResponse> GetNhatKysByUser(int userId);
        Task<GetNhatKyGetAllResponse> PH_GetNhatKysByUser(int userId);
        Task<SaveNhatKyResponse> SaveNhatKy(NhatKyDTO NhatKy);
        Task<DeleteNhatKyResponse> DeleteNhatKy(int NhatKyId, int userId);
        Task<UpdateNhatKyResponse> UpdateNhatKy(int NhatKyId, NhatKyDTO NhatKy);
        Task<NhatKyResponse> GetIDNhatKy(int userId, int NhatKyId);
        Task<NhatKyResponse> PH_GetIDNhatKyByStudent(int id);
        Task<List<UploadFileNhatKyRequest>> AddMultipleImageNhatKy(List<IFormFile> fileNhatKyModel, string idUser);
        Task<List<UploadFileNhatKyRequest>> UploadFileMultipleNhatKy(List<IFormFile> fileNhatKyModel, string idUser);
    }
}
