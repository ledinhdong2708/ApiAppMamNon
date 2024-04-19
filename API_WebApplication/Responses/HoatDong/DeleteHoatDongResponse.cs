using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.HoatDong
{
    public class DeleteHoatDongResponse: BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HoatDongId { get; set; }
    }
}
