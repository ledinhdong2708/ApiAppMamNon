using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.ThoiKhoaBieuReponse
{
    public class DeleteThoiKhoaBieuResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ThoiKhoaBieuId { get; set; }
    }
}
