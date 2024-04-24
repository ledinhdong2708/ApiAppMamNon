namespace API_WebApplication.Requests.NhatKy
{
    public class UploadMultipleFileImageNhatKy
    {
        public string? FileName { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
