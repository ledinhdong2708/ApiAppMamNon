using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.ThoiKhoaBieu
{
    public class DeleteThoiKhoaBieuResponseExample : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ThoiKhoaBieuId { get; set; }
    }
}
