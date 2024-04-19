using API_WebApplication.DTO.HocPhiModel;
using API_WebApplication.Models;
using API_WebApplication.Responses.HocPhiModel;
using API_WebApplication.Responses.HocPhis;
using AutoMapper.Configuration.Conventions;

namespace API_WebApplication.Interfaces.HocPhiModels
{
    public interface IHocPhiModelService
    {
        Task<GetHocPhiModel2Response> GetHocPhiModelsByUser(int userId);
        Task<SaveHocPhiModel2Response> SaveHocPhiModel(HocPhiModel2DTO HocPhiModel);
        Task<DeleteHocPhiModel2Response> DeleteHocPhiModel(int HocPhiModelId, int userId);
        Task<UpdateHocPhiModel2Response> UpdateHocPhiModel(int HocPhiModelId, HocPhiModel2DTO HocPhiModel);
        Task<HocPhiModel2Response> GetIDHocPhiModel(int userId, int HocPhiId);
        Task<UpdateHocPhiModel2_DetailResponse> UpdateHocPhiModel_Detail(int HocPhiModelId, HocPhiModel2 HocPhiModel);
        Task<GetHocPhiModel2Response> GetHocPhiModelByStudent(int userId, int studentId);
        Task<UpdateStatusHocPhiModelResponse> UpdateStatusHocPhi(int id, bool statusHocPhi);
        Task<GetHocPhiModel2Response>  PH_GetHocPhiModelByStudent(int userId ,int studentId);

        Task<NewHocPhiModel2Response> newHocPhiModel2Response(string idClass, string idKhoaHoc, decimal tenhoc, string month, int userId , decimal tienan , string years);
    
    }
}
