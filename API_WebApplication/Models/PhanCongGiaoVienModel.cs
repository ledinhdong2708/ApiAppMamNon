namespace API_WebApplication.Models
{
    public partial class PhanCongGiaoVienModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }

        public string? Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int ClassId { get; set; }

        public int KhoaHocId { get; set; }
        public bool IsCompleted { get; set; }

        public int? AppID { get; set; }
        public int? UserAdd { set; get; }

    }
}
