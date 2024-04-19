using API_WebApplication.Interfaces.TableLikeInterface;
using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.TableLikes;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.TableLikeServices
{
    public class TableLikeService : ITableLikeService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public TableLikeService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteTableLikeResponse> DeleteTableLike(int tablelikeId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var tableLike = await _aPI_Application_V1Context.TableLikes.Where(o => o.Id == tablelikeId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (tableLike == null)
            {
                return new DeleteTableLikeResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (tableLike.UserId != userId)
            {
                return new DeleteTableLikeResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.TableLikes.Remove(tableLike);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteTableLikeResponse
                {
                    Success = true,
                    TableLikeId = tableLike.Id
                };
            }
            return new DeleteTableLikeResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<SaveTableLikeResponse> SaveTableLike(TableLike tableLike)
        {
            await _aPI_Application_V1Context.TableLikes.AddAsync(tableLike);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveTableLikeResponse
                {
                    Success = true,
                    TableLike = tableLike
                };
            }
            return new SaveTableLikeResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
    }
}
