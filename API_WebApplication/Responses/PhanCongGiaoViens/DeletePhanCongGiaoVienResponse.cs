using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.PhanCongGiaoViens
{
    public class DeletePhanCongGiaoVienResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PhanCongGiaoVienId { get; set; }
    }
}
