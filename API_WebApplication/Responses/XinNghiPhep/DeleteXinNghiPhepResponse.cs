using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.XinNghiPhep
{
    public class DeleteHocPhiModelResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int XinNghiPhepId { get; set; }
    }
}
