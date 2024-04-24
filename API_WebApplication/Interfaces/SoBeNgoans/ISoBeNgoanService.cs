using API_WebApplication.Models;
using API_WebApplication.Responses.SoBeNgoans;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieuReponse;

namespace API_WebApplication.Interfaces.SoBeNgoans
{
    public interface ISoBeNgoanService
    {
        Task<GetSoBeNgoanResponse> GetSoBeNgoansByUser(int userId);
        Task<SaveSoBeNgoanResponse> SaveSoBeNgoan(SoBeNgoan sobengoan);
        Task<DeleteSoBeNgoanResponse> DeleteSoBeNgoan(int sobengoanId, int userId);
        Task<UpdateSoBeNgoanResponse> UpdateSoBeNgoan(int sobengoanId, SoBeNgoan sobengoan);
        Task<SoBeNgoanResponse> GetIDSoBeNgoan(int userId, int sobengoanId);
        Task<GetSoBeNgoanResponse> GetSoBeNgoanFillterByUserYearClass(int userId, string year, string classId, string month);
        #region Phụ huynh
        Task<SoBeNgoanResponse> PH_GetIDByStudent(int id, string month, string year, int userId);
        #endregion
    }
}
