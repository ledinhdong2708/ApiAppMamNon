using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.KhoaHocs
{
    public class DeleteKhoaHocResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int KhoaHocId { get; set; }
    }
}
