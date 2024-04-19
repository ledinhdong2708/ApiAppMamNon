using API_WebApplication.Models;
using API_WebApplication.Responses.Classs;

namespace API_WebApplication.Interfaces.Classs
{
    public interface IClassService
    {
        Task<GetClassResponse> GetClasssByUser(int userId);
        Task<SaveClassResponse> SaveClasss(ClassModel Classs);
        Task<DeleteClassResponse> DeleteClasss(int ClasssId, int userId);
        Task<UpdateClassResponse> UpdateClasss(int ClasssId, ClassModel Classs);
        Task<ClassResponse> GetIDClasss(int userId, int ClasssId);
    }
}
