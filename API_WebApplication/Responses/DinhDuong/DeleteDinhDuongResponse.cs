using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.DinhDuong
{
    public class DeleteDinhDuongResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DinhDuongId { get; set; }
    }
}
