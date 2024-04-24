namespace API_WebApplication.Requests.NhatKy
{
    public class UploadFileVideoNhatKy
    {
        public string? FileName { get; set; }

        public IFormFile? Attachments { get; set; }
    }
}
