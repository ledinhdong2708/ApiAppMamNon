using API_WebApplication.Models;

namespace API_WebApplication.Requests.PhanCongGiaoVien
{
    public class PhanCongGiaoVienRequest
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

        //public virtual ICollection<ClassModel> Classs { get; set; } = new List<ClassModel>();

        //public virtual ICollection<KhoaHocModel> KhoaHocs { get; set; } = new List<KhoaHocModel>();
    }
}
