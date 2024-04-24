using API_WebApplication.Models;

namespace API_WebApplication.DTO.HocPhis
{
    public class HocPhiDTO
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public int StudentId { get; set; }

        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public bool IsCompleted { get; set; }


        public decimal TotalMax { get; set; }

        public decimal TotalMin { get; set; }
        public int? AppID { get; set; }

        public virtual ICollection<ChiTietHocPhi> ChiTietHocPhis { get; set; } = new List<ChiTietHocPhi>();
    }
}
