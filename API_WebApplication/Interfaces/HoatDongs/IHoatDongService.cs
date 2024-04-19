using API_WebApplication.Models;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.HoatDong;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Interfaces.HoatDongs
{
    public interface IHoatDongService
    {
        Task<GetHoatDongResponse2> GetHoatDongByUser(int userId);
        Task<SaveHoatDongResponse> SaveHoatDong(HoatDongModel HoatDong);
        Task<DeleteHoatDongResponse> DeleteHoatDong(int HoatDongId, int userId);
        Task<UpdateHoatDongResponse> UpdateHoatDong(int HoatDongId, HoatDongModel TinTuc);
        Task<HoatDongResponse> GetIDHoatDong(int userId, int HoatDongId);
        Task<GetHoatDongResponse> PH_GetHoatDongBy(int userId);
        Task<string> UploadFile([FromForm] UploadFileStudentModel fileStudentModel, string idUser);
        Task<(byte[], string, string)> DownloadFile(string FileName);
        Task<GetHoatDongResponse2> GetStudentsFillterByUserKhoaHocClass(int userId, string khoaHocId, string classId);
        Task<HoatDongResponse> GV_GetHoatDongByUserId(int userId);
        Task<HoatDongResponse> PH_GetHoatDongByUserId(int userId);
    }
}
