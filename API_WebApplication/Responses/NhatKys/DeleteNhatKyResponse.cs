using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.NhatKys
{
    public class DeleteNhatKyResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int NhatKyId { get; set; }
    }
}
