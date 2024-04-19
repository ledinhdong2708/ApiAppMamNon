using API_WebApplication.Models;

namespace API_WebApplication.DTO.ThoiKhoaBieus
{
    public class TKB1DTO
    {
        public int Id { get; set; }

        public string Thu { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int OtkbId { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public int ParentId { get; set; }

        public int Branch { get; set; }
        public int? AppID { get; set; }

        //public virtual Otkb Otkb { get; set; }
    }
}
