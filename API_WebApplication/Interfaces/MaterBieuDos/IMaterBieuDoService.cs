using API_WebApplication.Models;
using API_WebApplication.Responses.MaterBieuDos;

namespace API_WebApplication.Interfaces.MaterBieuDos
{
    public interface IMaterBieuDoService
    {
        Task<GetMaterBieuDoResponse> GetMaterBieuDoByUser(int userId);
        Task<SaveMaterBieuDoResponse> SaveMaterBieuDo(MaterBieuDo MaterBieuDo);
        Task<DeleteMaterBieuDoResponse> DeleteMaterBieuDo(int MaterBieuDoId, int userId);
        Task<UpdateMaterBieuDoResponse> UpdateMaterBieuDo(int MaterBieuDoId, MaterBieuDo MaterBieuDo);
        Task<MaterBieuDoResponse> GetIDMaterBieuDo(int userId, int MaterBieuDoId);
        Task<GetMaterBieuDoResponse> GetMaterBieuDoByStudent(int userId, int studentId);
        Task<GetMaterBieuDoResponse> PH_GetMaterBieuDoByStudent(int id, int userId);
    }
}
