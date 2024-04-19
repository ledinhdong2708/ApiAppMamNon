using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.HocPhiModel
{
    public class DeleteHocPhiModel2Response : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HocPhiModelId { get; set; }
    }
}
