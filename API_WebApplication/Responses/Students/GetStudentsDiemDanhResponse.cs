using API_WebApplication.Models;

namespace API_WebApplication.Responses.Students
{
    public class GetStudentsDiemDanhResponse : BaseResponse
    {
        public List<StudentDiemDanh> StudentDiemDanhs { get; set; }
    }

    public partial class StudentDiemDanh
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }

        public string? NameStudent { get; set; }

        public string? Year1 { get; set; }

        public string? Class1 { get; set; }

        public string? Year2 { get; set; }

        public string? Class2 { get; set; }

        public string? Year3 { get; set; }

        public string? Class3 { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }

        public string? img { get; set; }

        public string? GV1 { get; set; }

        public string? GV2 { get; set; }

        public string? GV3 { get; set; }

        public decimal? CanNang { get; set; }

        public decimal? ChieuCao { get; set; }

        public int? SoBeNgoanId { get; set; }

        public int? PhuHuynhId { get; set; }

        public string? imagePatch { get; set; }

        public bool? GioiTinh { get; set; }

        public int? AppID { get; set; }

        public bool? DenLop { get; set; }

        public bool? CoPhep { get; set; }

        public bool? KhongPhep { get; set; }
        public int idDiemDanh { get; set; }

        public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

        //public virtual ICollection<SoBeNgoan> SoBeNgoans { get; set; } = new List<SoBeNgoan>();
        public virtual SoBeNgoan? SoBeNgoan { get; set; }


        public virtual ICollection<TableImage> TableImages { get; set; } = new List<TableImage>();

        public virtual ICollection<TableLike> TableLikes { get; set; } = new List<TableLike>();


    }
}
