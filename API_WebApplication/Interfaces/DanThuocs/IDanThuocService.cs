using API_WebApplication.Models;
using API_WebApplication.Responses.DanThuocs;

namespace API_WebApplication.Interfaces.DanThuocs
{
    public interface IDanThuocService
    {
        Task<GetDanThuocResponse> GetDanThuocByUser(int userId);
        Task<SaveDanThuocResponse> SaveDanThuoc(DanThuocModel DanThuoc);
        Task<SaveDanThuocResponse> PH_SaveDanThuoc(DanThuocModel DanThuoc);
        Task<DeleteDanThuocResponse> DeleteDanThuoc(int DanThuocId, int userId);
        Task<UpdateDanThuocResponse> UpdateDanThuoc(int DanThuocId, DanThuocModel DanThuoc);
        Task<UpdateDanThuocResponse> PH_UpdateDanThuoc(int DanThuocId, DanThuocModel DanThuoc);
        Task<DanThuocResponse> GetIDDanThuoc(int userId, int DanThuocId);
        Task<GetDanThuocResponse> GetDanThuocByStudent(int userId, int studentId, string day, string month, string year);
        Task<GetDanThuocResponse> PH_GetDanThuocByStudent(int userId, int studentId, string day, string month, string year);
    }
}
