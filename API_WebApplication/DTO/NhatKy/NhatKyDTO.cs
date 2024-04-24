using API_WebApplication.Models;

namespace API_WebApplication.DTO.NhatKy
{
    public class NhatKyDTO
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int UserId { get; set; }

        public int TableLikeId { get; set; }

        public int TableImageId { get; set; }

        public int ClassId { get; set; }

        public int KhoaId { get; set; }

        public int BinhLuanId { get; set; }

        public string? StudentId { get; set; }

        public int? AppID { get; set; }

        public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

        public virtual ICollection<TableImage> TableImages { get; set; } = new List<TableImage>();

        public virtual ICollection<TableLike> TableLikes { get; set; } = new List<TableLike>();
    }
}
