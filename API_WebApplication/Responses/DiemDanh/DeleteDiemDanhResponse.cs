using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.DiemDanh
{
    public class DeleteDiemDanhResponse: BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DiemDanhId { get; set; }
    }
}
