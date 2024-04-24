using API_WebApplication.Models;

namespace API_WebApplication.Responses.HoatDong
{
    public class GetHoatDongResponse2: BaseResponse
    {
        public List<HoatDongModel2>? HoatDongModels { get; set; }
    }
    public partial class HoatDongModel2
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }

        public string? Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }

        public int? AppID { get; set; }
        public string? Img { get; set; }
        public int? ClassID { get; set; }
        public string? Months { get; set; }
        public string? Years { get; set; }
        public ClassModel? Classs { get; set; }
        public int? KhoaHocId { get; set; }
        public KhoaHocModel? KhoaHoc { get; set; }
    }
}
