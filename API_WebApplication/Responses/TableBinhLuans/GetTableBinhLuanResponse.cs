using API_WebApplication.Models;

namespace API_WebApplication.Responses.TableBinhLuans
{
    public class GetTableBinhLuanResponse : BaseResponse
    {
        public List<BinhLuan> BinhLuans { get; set; }
    }
}
