using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;

namespace API_WebApplication.Interfaces.DinhDuongs
{
    public interface IDinhDuongService
    {
        Task<GetDinhDuongResponse> GetDinhDuongByUser(int userId);
        Task<SaveDinhDuongResponse> SaveDinhDuong(DinhDuongModel dinhduong);
        Task<DeleteDinhDuongResponse> DeleteDinhDuong(int dinhduongId, int userId);
        Task<UpdateDinhDuongResponse> UpdateDinhDuong(int dinhduongId, DinhDuongModel dinhduong);
        Task<DinhDuongResponse> GetIDDinhDuong(int userId, int dinhduongId);
        Task<DinhDuongResponse> GetIDDinhDuongByDate(int userId, int day, int month, int year, string khoahocid, string classid);
        Task<DinhDuongResponse> PH_GetIDDinhDuongByDate(int day, int month, int year, int userId);
    }
}
