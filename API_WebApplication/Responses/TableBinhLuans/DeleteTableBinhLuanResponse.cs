using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.TableBinhLuans
{
    public class DeleteTableBinhLuanResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TableBinhLuanId { get; set; }
    }
}
