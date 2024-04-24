namespace API_WebApplication.Requests.Students
{
    public class UploadFileStudentModel
    {
        public string FileName { get; set; }
        public IFormFile file { get; set; }
    }
}
