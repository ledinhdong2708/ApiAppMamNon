using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.SoBeNgoans
{
    public class DeleteSoBeNgoanResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int SoBeNgoanId { get; set; }
    }
}
