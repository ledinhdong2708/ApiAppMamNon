using API_WebApplication.Models;

namespace API_WebApplication.Requests.NhatKy
{
    public class UploadFileNhatKyRequest
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePatch { get; set; }
        public string? UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string? AppID { get; set; }
    }
}
