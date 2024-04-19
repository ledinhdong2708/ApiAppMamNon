
using API_WebApplication.Models;
using API_WebApplication.Responses.MaterHocPhis;

namespace API_WebApplication.Interfaces.MaterHocPhis
{
    public interface IMaterHocPhiService
    {
        Task<GetMaterHocPhiResponse> GetMaterHocPhiByUser(int userId);
        Task<SaveMaterHocPhiResponse> SaveMaterHocPhi(MaterHocPhi MaterHocPhi);
        Task<DeleteMaterHocPhiResponse> DeleteMaterHocPhi(int MaterHocPhiId, int userId);
        Task<UpdateMaterHocPhiResponse> UpdateMaterHocPhi(int MaterHocPhiId, MaterHocPhi MaterHocPhi);
        Task<MaterHocPhiResponse> GetIDMaterHocPhi(int userId, int MaterHocPhiId);
        Task<GetMaterHocPhiResponse> GetMaterHocPhiByStudent(int userId, int studentId);
        Task<GetMaterHocPhiResponse> PH_GetMaterHocPhiByStudent(int id, int userId);
        Task<GetMaterHocPhiResponse> GetMaterHocPhiFalseByUser(int userId);
    }
}
