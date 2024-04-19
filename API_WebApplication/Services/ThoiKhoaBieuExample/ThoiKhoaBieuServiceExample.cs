using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.Interfaces.ThoiKhoaBieu;
using API_WebApplication.Models;
using API_WebApplication.Responses.Students;
using API_WebApplication.Responses.ThoiKhoaBieu;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_WebApplication.Services.ThoiKhoaBieu
{
    public class ThoiKhoaBieuServiceExample : IThoiKhoaBieuServiceExample
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly IMapper _mapper;
        public ThoiKhoaBieuServiceExample(API_Application_V1Context aPI_Application_V1Context, IMapper mapper)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            this._mapper = mapper;
        }
        public async Task<ThoiKhoaBieuResponseExample> GetIDThoiKhoaBieu(int userId, int thoikhoabieuId)
        {
            var thoiKhoaBieu = await _aPI_Application_V1Context.OTKBs.Include(_ => _.TKB1s).Where(_ => _.Id == thoikhoabieuId).FirstOrDefaultAsync();
            if (thoiKhoaBieu == null)
            {
                return new ThoiKhoaBieuResponseExample {
                    Success = false,
                    Error = "Thoi khoa bieu not found",
                    ErrorCode = "T01"
                };
            }
            if (thoiKhoaBieu.UserId != userId)
            {
                return new ThoiKhoaBieuResponseExample
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (thoiKhoaBieu.Id < 0)
            {
                return new ThoiKhoaBieuResponseExample
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new ThoiKhoaBieuResponseExample
            {
                Success = true,
                Data = thoiKhoaBieu
            };
        }

        public async Task<DeleteThoiKhoaBieuResponseExample> DeleteThoiKhoaBieu(int thoikhoabieuId, int userId)
        {
            var thoikhoabieu = await _aPI_Application_V1Context.OTKBs.FindAsync(thoikhoabieuId);
            if (thoikhoabieu == null)
            {
                return new DeleteThoiKhoaBieuResponseExample
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (thoikhoabieu.UserId != userId)
            {
                return new DeleteThoiKhoaBieuResponseExample
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.OTKBs.Remove(thoikhoabieu);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteThoiKhoaBieuResponseExample
                {
                    Success = true,
                    ThoiKhoaBieuId = thoikhoabieu.Id
                };
            }
            return new DeleteThoiKhoaBieuResponseExample
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }

        public async Task<GetThoiKhoaBieusResponseExample> GetThoiKhoaBieusByUser(int userId)
        {
            var thoiKhoaBieu = await _aPI_Application_V1Context.OTKBs.Where(_=>_.UserId == userId).Include(_ => _.TKB1s).ToListAsync();
            if (thoiKhoaBieu.Count == 0)
            {
                return new GetThoiKhoaBieusResponseExample
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04",
                    Data = thoiKhoaBieu.ToList()
                };
            }
            return new GetThoiKhoaBieusResponseExample { Success = true, otkbs = thoiKhoaBieu };
        }

        public async Task<SaveThoiKhoaBieuResponseExample> SaveThoiKhoaBieu(OTKBDTO _otkbdto)
        {
            var newTKB = _mapper.Map<OTKB>(_otkbdto);
            await _aPI_Application_V1Context.AddAsync(newTKB);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            //return Created($"/ThoiKhoaBieu/{newTKB.Id}", newTKB);

            //await _aPI_Application_V1Context.Student.AddAsync(student);
            //var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveThoiKhoaBieuResponseExample
                {
                    Success = true,
                    Data = newTKB
                };
            }
            return new SaveThoiKhoaBieuResponseExample
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }

        public async Task<UpdateThoiKhoaBieuResponseExample> UpdateThoiKhoaBieu(int thoikhoabieuId, OTKBDTO _otkbdto)
        {
            //var thoikhoabieuById = await _aPI_Application_V1Context.Otkb.Include(_ => _.Tkb1).Where(_ => _.Id == thoikhoabieuId).FirstOrDefaultAsync();
            //if (thoikhoabieuById == null)
            //{
            //    return new UpdateThoiKhoaBieuResponse
            //    {
            //        Success = false,
            //        Error = "Thoi Khoa Bieu not found",
            //        ErrorCode = "T01"
            //    };
            //}
            //if (thoikhoabieuById.UserId != thoikhoabieuById.UserId)
            //{
            //    return new UpdateThoiKhoaBieuResponse
            //    {
            //        Success = false,
            //        Error = "You don't have access to get id this student",
            //        ErrorCode = "T02"
            //    };
            //}

            var updateOTKB = _mapper.Map<OTKB>(_otkbdto);
            _aPI_Application_V1Context.Update(updateOTKB);
            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateThoiKhoaBieuResponseExample
                {
                    Success = true,
                    otkb = _otkbdto
                };
            }
            return new UpdateThoiKhoaBieuResponseExample
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
