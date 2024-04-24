using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.TinTuc
{
    public class DeleteTinTucResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TinTucId { get; set; }
    }
}
