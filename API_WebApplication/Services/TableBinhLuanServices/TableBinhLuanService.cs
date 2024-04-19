using API_WebApplication.Interfaces.TableBinhLuanInterface;
using API_WebApplication.Models;
using API_WebApplication.Requests.TableBinhLuan;
using API_WebApplication.Responses.KhoaHocs;
using API_WebApplication.Responses.TableBinhLuans;
using API_WebApplication.Responses.TableLikes;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_WebApplication.Services.TableBinhLuanServices
{
    public class TableBinhLuanService : ITableBinhLuanService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public TableBinhLuanService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteTableBinhLuanResponse> DeleteTableBinhLuan(int binhLuanId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var binhLuan = await _aPI_Application_V1Context.BinhLuans.Where(o => o.Id == binhLuanId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            if (binhLuan == null)
            {
                return new DeleteTableBinhLuanResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (binhLuan.UserId != userId)
            {
                return new DeleteTableBinhLuanResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.BinhLuans.Remove(binhLuan);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteTableBinhLuanResponse
                {
                    Success = true,
                    TableBinhLuanId = binhLuan.Id
                };
            }
            return new DeleteTableBinhLuanResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetTableBinhLuanResponse> GetTableBinhLuanByIDNhatKy(int idNhatKy, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var binhLuans = await _aPI_Application_V1Context.BinhLuans.Where(o => o.AppID == appID!.AppID && o.NhatKyId == idNhatKy)
            .Select(o => new BinhLuan
                {
                    Content = o.Content,
                    CreateDate = o.CreateDate,
                    Status = o.Status,
                    NhatKyId = o.NhatKyId,
                    UserId = o.UserId,
                    StudentId = o.StudentId,
                    AppID = o.AppID,
                    User = o.User,
                    Student = _aPI_Application_V1Context.Students.Where(a => a.Id == o.StudentId).FirstOrDefault()
                }).OrderByDescending(c => c.CreateDate).ToListAsync();
            return new GetTableBinhLuanResponse { Success = true, BinhLuans = binhLuans.ToList() };
        }

        public async Task<SaveTableBinhLuanResponse> SaveTableBinhLuan(BinhLuan binhLuan)
        {
            await _aPI_Application_V1Context.BinhLuans.AddAsync(binhLuan);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveTableBinhLuanResponse
                {
                    Success = true,
                    BinhLuan = binhLuan
                };
            }
            return new SaveTableBinhLuanResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
    }
}
