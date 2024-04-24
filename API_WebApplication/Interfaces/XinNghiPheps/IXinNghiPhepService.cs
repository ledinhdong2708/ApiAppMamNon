using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Models;
using API_WebApplication.Responses.XinNghiPhep;

namespace API_WebApplication.Interfaces.XinNghiPheps
{
    public interface IXinNghiPhepService
    {
        Task<GetHocPhiModelResponse> GetXinNghiPhepsByUser(int userId);
        Task<SaveHocPhiModelResponse> SaveXinNghiPhep(XinNghiPhepDTO XinNghiPhep);
        Task<SaveHocPhiModelResponse> PH_SaveXinNghiPhep(XinNghiPhepDTO XinNghiPhep);
        Task<DeleteHocPhiModelResponse> DeleteXinNghiPhep(int XinNghiPhepId, int userId);
        Task<UpdateHocPhiModelResponse> UpdateXinNghiPhep(int XinNghiPhepId, XinNghiPhepDTO XinNghiPhep);
        Task<UpdateHocPhiModelResponse> PH_UpdateXinNghiPhep(int XinNghiPhepId, XinNghiPhepDTO XinNghiPhep);
        Task<HocPhiModelResponse> GetIDXinNghiPhep(int userId, int XinNghiPhepId);
        Task<UpdateHocPhiModel_DetailResponse> UpdateXinNghiPhep_Detail(int XinNghiPhepId, ChiTietXinNghiPhep XinNghiPhep);
        Task<UpdateHocPhiModel_DetailResponse> PH_UpdateXinNghiPhep_Detail(int XinNghiPhepId, ChiTietXinNghiPhep XinNghiPhep);
        Task<GetHocPhiModelResponse2> GetIDXinNghiPhepByDate(int userId, int day, int month, int year);
        Task<HocPhiModelResponse> PH_GetIDXinNghiPhepByStudent(int id, int userId);
    }
}
