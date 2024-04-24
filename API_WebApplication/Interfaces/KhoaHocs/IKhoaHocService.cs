using API_WebApplication.Models;
using API_WebApplication.Responses.KhoaHocs;

namespace API_WebApplication.Interfaces.KhoaHocs
{
    public interface IKhoaHocService
    {
        Task<GetKhoaHocResponse> GetKhoaHocByUser(int userId);
        Task<SaveKhoaHocResponse> SaveKhoaHoc(KhoaHocModel KhoaHoc);
        Task<DeleteKhoaHocResponse> DeleteKhoaHoc(int KhoaHocId, int userId);
        Task<UpdateKhoaHocResponse> UpdateKhoaHoc(int KhoaHocId, KhoaHocModel KhoaHoc);
        Task<KhoaHocResponse> GetIDKhoaHoc(int userId, int KhoaHocId);
    }
}
