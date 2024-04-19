using API_WebApplication.Interfaces.Classs;
using API_WebApplication.Models;
using API_WebApplication.Responses.Classs;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.Classs
{
    public class ClassService : IClassService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public ClassService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteClassResponse> DeleteClasss(int ClasssId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o=>o.Id == userId).FirstOrDefaultAsync();
            var Class = await _aPI_Application_V1Context.ClassModels.Where(o=>o.Id == ClasssId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var Class = await _aPI_Application_V1Context.ClassModels.FindAsync(ClasssId);
            if (Class == null)
            {
                return new DeleteClassResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (Class.UserId != userId)
            {
                return new DeleteClassResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.ClassModels.Remove(Class);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteClassResponse
                {
                    Success = true,
                    ClassId = Class.Id
                };
            }
            return new DeleteClassResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetClassResponse> GetClasssByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).Select(o => o.AppID).FirstOrDefaultAsync();
            var classes = await _aPI_Application_V1Context.ClassModels.Where(o => o.AppID == appID).ToListAsync();
            return new GetClassResponse { Success = true, Classss = classes.ToList() };
        }

        public async Task<ClassResponse> GetIDClasss(int userId, int ClasssId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var _Class = await _aPI_Application_V1Context.ClassModels.Where(o => o.Id == ClasssId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (_Class == null)
            {
                return new ClassResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (_Class.UserId != userId)
            {
                return new ClassResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (_Class.Id < 0)
            {
                return new ClassResponse
                {
                    Success = false,
                    Error = "No ThoiKhoaBieu found for this user",
                    ErrorCode = "T04"
                };
            }
            return new ClassResponse
            {
                Success = true,
                Classs = _Class
            };
        }

        public async Task<SaveClassResponse> SaveClasss(ClassModel Classs)
        {
            await _aPI_Application_V1Context.ClassModels.AddAsync(Classs);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveClassResponse
                {
                    Success = true,
                    Classs = Classs
                };
            }
            return new SaveClassResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateClassResponse> UpdateClasss(int ClasssId, ClassModel Classs)
        {
            var ClassById = await _aPI_Application_V1Context.ClassModels.FindAsync(ClasssId);
            if (ClassById == null)
            {
                return new UpdateClassResponse
                {
                    Success = false,
                    Error = "ThoiKhoaBieu not found",
                    ErrorCode = "T01"
                };
            }
            if (Classs.UserId != ClassById.UserId)
            {
                return new UpdateClassResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this ThoiKhoaBieu",
                    ErrorCode = "T02"
                };
            }

            ClassById.NameClass = Classs.NameClass;
            ClassById.UpdateDate = Classs.UpdateDate;
            ClassById.IsCompleted = Classs.IsCompleted;
            ClassById.AppID = Classs.AppID;

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateClassResponse
                {
                    Success = true,
                    Classs = Classs
                };
            }
            return new UpdateClassResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
