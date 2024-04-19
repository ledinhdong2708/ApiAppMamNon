using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.MaterBieuDos
{
    public class DeleteMaterBieuDoResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MaterBieuDoId { get; set; }
    }
}
