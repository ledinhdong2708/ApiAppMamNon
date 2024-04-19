using API_WebApplication.Models;

namespace API_WebApplication.Requests.XinNghiPhep
{
    public class XinNghiPhepRequest
    {
        public int Id { get; set; }

        public int Role { get; set; }

        public int StudentId { get; set; }

        public string? Content { get; set; }

        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }

    }
}
