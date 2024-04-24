using API_WebApplication.Models;
using API_WebApplication.Responses.PhanCongGiaoViens;

namespace API_WebApplication.Interfaces.PhanCongGiaoVienModels
{
    public interface IPhanCongGiaoVienService
    {
        Task<GetPhanCongGiaoVienResponse> GetPhanCongGiaoVienByUser(int userId, string classsid, string khoahocid);
        Task<SavePhanCongGiaoVienResponse> SavePhanCongGiaoVien(PhanCongGiaoVienModel PhanCongGiaoVienModel);
        Task<SavePhanCongGiaoVienResponse> PH_SavePhanCongGiaoVien(PhanCongGiaoVienModel PhanCongGiaoVienModel);
        Task<DeletePhanCongGiaoVienResponse> DeletePhanCongGiaoVien(int PhanCongGiaoVienModelId, int userId);
        Task<UpdatePhanCongGiaoVienResponse> UpdatePhanCongGiaoVien(int PhanCongGiaoVienModelId, PhanCongGiaoVienModel PhanCongGiaoVienModel);
        Task<UpdatePhanCongGiaoVienResponse> PH_UpdatePhanCongGiaoVien(int PhanCongGiaoVienModelId, PhanCongGiaoVienModel PhanCongGiaoVienModel);
        Task<PhanCongGiaoVienResponse> GetIDPhanCongGiaoVien(int userId, int PhanCongGiaoVienModelId);
        Task<GetPhanCongGiaoVienResponse> GetPhanCongGiaoVienByStudent(int userId,int id);
        Task<GetPhanCongGiaoVienResponse> PH_GetPhanCongGiaoVienByStudent(int userId, int classid);
    }
}
