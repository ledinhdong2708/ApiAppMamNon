 using API_WebApplication.Models;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.Students;
using Microsoft.AspNetCore.Mvc;

namespace API_WebApplication.Interfaces.Students
{
    public interface IStudentService
    {
        Task<GetStudentsResponse> GetStudentsByUser(int userId);
        Task<SaveStudentResponse> SaveStudent(Student student);
        Task<DeleteStudentResponse> DeleteStudent(int studentId, int userId);
        Task<UpdateStudentResponse> UpdateStudent(int studentId, Student student);
        Task<StudentResponse> GetIDStudent(int userId, int studentId);
        Task<GetStudentsResponse> GetAllStudentInClass(string classID, int userId);
        Task<StudentResponse> GetIDByStudent(int studentId, int userId);
        Task<GetStudentsResponse> GetAllStudentInNameClass(string nameClass , int userId);
        Task<GetStudentsResponse> GetStudentsFillterByUserKhoaHocClass(int userId, string khoaHocId, string classId);
        Task<UpdateStudentResponse> UploadImageStudent(int studentId, [FromForm] UploadFileStudentModel fileStudentModel, string idUser);
        Task<GetStudentsDiemDanhResponse> GetStudentsFillterByUserKhoaHocClassDate(int userId, string khoaHocId, string classId, string day, string month, string year);
    }
}
