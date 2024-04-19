using System.Text.Json.Serialization;

namespace API_WebApplication.Responses.Students
{
    public class DeleteStudentResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int StudentId { get; set; }
    }
}
