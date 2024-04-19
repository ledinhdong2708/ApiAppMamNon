using API_WebApplication.Interfaces.GiaTiens;
using API_WebApplication.Models;
using API_WebApplication.Responses.GiaTiens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_WebApplication.Services.GiaTiens
{
    public class GiaTiensService : IGiaTienService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public GiaTiensService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }


        public async Task<getallItemResponse> getallItemResponse()
        {
            try
            {
                var existingGiatien = await _aPI_Application_V1Context.GiaTiens.ToArrayAsync();
                return new getallItemResponse { Success = true, giaTiensModels = existingGiatien.ToList() };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new getallItemResponse { Success = false, Error = ex.Message };
            }
        }

        public async Task<NewGiaTiensResponse> NewGiatiensResponse(int IdGia, GiaTiensModel giaTiensModel)
        {
          
            DateTime dateTime = DateTime.Now;
            var existingGiatien = await _aPI_Application_V1Context.GiaTiens.FirstOrDefaultAsync(c => c.Id == IdGia);

            if (existingGiatien != null)
            {
                return new NewGiaTiensResponse
                {
                    Success = false,
                    Error = "Id already exists",
                    ErrorCode = "T01"
                };
            }
            var giatien = new GiaTiensModel
            {
                name = giaTiensModel.name, 
                gia = giaTiensModel.gia, 
                CreateDate = dateTime,
                UpdateDate = dateTime
            };

            // Thêm bản ghi mới vào DbContext
            await _aPI_Application_V1Context.AddAsync(giatien);

            // Lưu thay đổi vào cơ sở dữ liệu
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse > 0)
            {
                // Trả về kết quả thành công nếu lưu thành công
                return new NewGiaTiensResponse { Success = true, GiaTiensModel = giatien };
            }
            else
            {
                // Trả về lỗi nếu không thể lưu vào cơ sở dữ liệu
                return new NewGiaTiensResponse
                {
                    Success = false,
                    Error = "Unable to save the record",
                    ErrorCode = "S06"
                };
            }

        }


        public async Task<UpdateGiaTiensResponse> UpdateGiaTiensResponse(int idGia, GiaTiensModel gia)
        {
            var GiaTienId = await _aPI_Application_V1Context.GiaTiens.FindAsync(idGia);
            if (GiaTienId == null)
            {
                return new UpdateGiaTiensResponse
                {
                    Success = false,
                    Error = "Not found",
                    ErrorCode = "T01"
                };
            }
            GiaTienId.gia = gia.gia;
            GiaTienId.UpdateDate = DateTime.Now;

             var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateGiaTiensResponse
                {
                    Success = true,
                    giaTiensModels = gia
                };
            }
            return new UpdateGiaTiensResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
    }
}
