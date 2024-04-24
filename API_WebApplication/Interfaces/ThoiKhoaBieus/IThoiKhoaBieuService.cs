using API_WebApplication.Models;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;

namespace API_WebApplication.Interfaces.ThoiKhoaBieus
{
    public interface IThoiKhoaBieuService
    {
        Task<GetThoiKhoaBieuResponse> GetThoiKhoaBieuByUser(int userId);
        Task<SaveThoiKhoaBieuResponse> SaveThoiKhoaBieu(ThoiKhoaBieuModel thoikhoabieu);
        Task<DeleteThoiKhoaBieuResponse> DeleteThoiKhoaBieu(int thoikhoabieuId, int userId);
        Task<UpdateThoiKhoaBieuResponse> UpdateThoiKhoaBieu(int thoikhoabieuId, ThoiKhoaBieuModel thoikhoabieu);
        Task<ThoiKhoaBieuResponse> GetIDThoiKhoaBieu(int userId, int thoikhoabieuId);
        Task<GetThoiKhoaBieuResponse> GetStudentsFillterByUserDateClass(int userId, string day, string month, string year, string classId);
        #region Phụ huynh
        Task<ThoiKhoaBieuResponse> PH_GetIDByStudent(string day, string month, string year,int id, int userId);
        #endregion End Phụ Huynh
    }
}
