using API_WebApplication.Models;

namespace API_WebApplication.DTO.NhatKy
{
    public class TableImageDTO
    {
        public int Id { get; set; }

        public string? ImageName { get; set; }

        public string? ImagePatch { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int NhatKyId { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public int? AppID { get; set; }

        public virtual NhatKyDTO? NhatKy { get; set; }

        public virtual Student? Student { get; set; }

        public virtual User? User { get; set; }
    }
}
