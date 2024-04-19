using API_WebApplication.Models;
using API_WebApplication.Responses.DinhDuong;
using API_WebApplication.Responses.TableLikes;

namespace API_WebApplication.Interfaces.TableLikeInterface
{
    public interface ITableLikeService
    {
        Task<SaveTableLikeResponse> SaveTableLike(TableLike tableLike);
        Task<DeleteTableLikeResponse> DeleteTableLike(int tablelikeId, int userId);
    }
}
