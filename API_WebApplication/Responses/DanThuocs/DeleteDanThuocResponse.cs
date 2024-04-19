using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.DanThuocs
{
    public class DeleteDanThuocResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DanThuocId { get; set; }
    }
}
