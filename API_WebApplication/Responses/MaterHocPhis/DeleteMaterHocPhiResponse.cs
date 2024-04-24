using API_WebApplication.Models;
using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.MaterHocPhis
{
    public class DeleteMaterHocPhiResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MaterHocPhiId { get; set; }
    }
}
