using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.Classs
{
    public class DeleteClassResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ClassId { get; set; }
    }
}
