using API_WebApplication.Models;
using API_WebApplication.Requests.DiemDanh;
using API_WebApplication.Responses.DiemDanh;

namespace API_WebApplication.Interfaces.DiemDanhs
{
    public interface IDiemDanhService
    {
        Task<GetDiemDanhResponse> GetDiemDanhByUser(int userId);
        Task<DiemDanhResponse> GetDiemDanhByDateAndIDStudent(int studentId, int userId);
        Task<SaveDiemDanhResponse> SaveDiemDanh(DiemDanhModel DiemDanh);
        Task<SaveDiemDanhResponse> SaveDiemDanhMultiple(List<DiemDanhModel> dataDiemDanh, int UserId);
        Task<DeleteDiemDanhResponse> DeleteDiemDanh(int DiemDanhId, int userId);
        Task<UpdateDiemDanhResponse> UpdateDiemDanh(int DiemDanhId, DiemDanhModel DiemDanh);
        Task<DiemDanhResponse> GetIDDiemDanh(int userId, int DiemDanhId);
        Task<GetDiemDanhResponse> GetDiemDanhStatus(int userId, string day, string month, string year);
        Task<GetDiemDanhResponse> UpdateDiemDanhMultiple(int UserId, List<DiemDanhRequest> dataDiemDanh);
    }
}
