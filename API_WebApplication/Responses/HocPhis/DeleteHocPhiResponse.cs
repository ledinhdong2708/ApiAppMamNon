using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.HocPhis
{
    public class DeleteHocPhiResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HocPhiId { get; set; }
    }
}
