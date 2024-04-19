using API_WebApplication.Models;

namespace API_WebApplication.Responses.HocPhiModel
{
    public class GetHocPhiModel2Response2: BaseResponse
    {
        public List<HocPhiModelModel2>? HocPhiModels { get; set; }
    }
    public partial class HocPhiModelModel2
    {
        public int Id { get; set; }

        public int Role { get; set; }

        public int StudentId { get; set; }

        public string? Content { get; set; }

        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public Student? Student { get; set; }

        public virtual ICollection<HocPhiChiTietModel2> ChiTietHocPhiModels { get; set; } = new List<HocPhiChiTietModel2>();
    }
}
