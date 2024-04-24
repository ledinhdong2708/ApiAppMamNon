namespace API_WebApplication.Models;

public partial class DinhDuongModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Role { get; set; }

    public DateTime? DocDate { get; set; }

    public string? BuoiSang { get; set; }

    public string? BuoiTrua { get; set; }

    public string? BuoiChinhChieu { get; set; }

    public string? BuoiPhuChieu { get; set; }

    public string? Dam { get; set; }

    public string? DamDinhMuc { get; set; }

    public string? Beo { get; set; }

    public string? BeoDinhMuc { get; set; }

    public string? Duong { get; set; }

    public string? DuongDinhMuc { get; set; }

    public string? NangLuong { get; set; }

    public string? NangLuongDinhMuc { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsCompleted { get; set; }

    public int? AppID { get; set; }
    public int? KhoaHocID { get; set; }
    public int? ClassID { get; set; }
}
