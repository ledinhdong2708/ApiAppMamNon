using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.AppIDs
{
    public class DeleteAppIDResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int appID_ID { get; set; }
    }
}
