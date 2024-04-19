using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.TableBinhLuans;
using API_WebApplication.Responses.TableLikes;

namespace API_WebApplication.Interfaces.TableBinhLuanInterface
{
    public interface ITableBinhLuanService
    {
        Task<GetTableBinhLuanResponse> GetTableBinhLuanByIDNhatKy(int idNhatKy, int userId);
        Task<SaveTableBinhLuanResponse> SaveTableBinhLuan(BinhLuan binhLuan);
        Task<DeleteTableBinhLuanResponse> DeleteTableBinhLuan(int binhLuanId, int userId);
    }
}
