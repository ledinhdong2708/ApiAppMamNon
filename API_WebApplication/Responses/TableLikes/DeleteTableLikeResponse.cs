using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.TableLikes
{
    public class DeleteTableLikeResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TableLikeId { get; set; }
    }
}
