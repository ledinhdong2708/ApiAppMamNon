using API_WebApplication.DTO.HocPhis;
using API_WebApplication.Models;
using API_WebApplication.Responses.HocPhis;
using API_WebApplication.Responses.SoBeNgoans;

namespace API_WebApplication.Interfaces.HocPhis
{
    public interface IHocPhiService
    {
        Task<GetHocPhiResponse> GetHocPhisByUser(int userId);
        Task<SaveHocPhiResponse> SaveHocPhi(HocPhiDTO HocPhi);
        Task<DeleteHocPhiResponse> DeleteHocPhi(int HocPhiId, int userId);
        Task<UpdateHocPhiResponse> UpdateHocPhi(int HocPhiId, HocPhiDTO HocPhi);
        Task<HocPhiResponse> GetIDHocPhi(int userId, int HocPhiId);
        Task<UpdateHocPhi_DetailResponse> UpdateHocPhi_Detail(int HocPhiId, HocPhi HocPhi);
        Task<GetHocPhiChiTietTheoMonthResponse> GetHocPhiChiTietTheoMonthByIDHocPhiChiTiet(int hocphiId, int userId);
        Task<GetHocPhiResponse> GetSoBeNgoanFillterByUserKhoaHocClassStudent(int userId, string year, string classId);
        Task<HocPhiResponse> PH_GetIDByHocPhiStudent(int id, int userId);
        Task<UpdateChiTietHocPhi> UpdateCompleteChiTietHocPhi(int id, bool statusHocPhi);
    }
}
