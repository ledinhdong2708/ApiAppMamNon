namespace API_WebApplication.Requests.SoBeNgoan
{
    public class SoBeNgoanRequest
    {
        public int? MonthSBN { get; set; }

        public bool? Tuan1 { get; set; }

        public bool? Tuan2 { get; set; }

        public bool? Tuan3 { get; set; }

        public bool? Tuan4 { get; set; }

        public bool? Tuan5 { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }

        public string? NhanXet { get; set; }

        public decimal? ChieuCao { get; set; }

        public decimal? CanNang { get; set; }

        public string? ClassSBN { get; set; }

        public int? YearSBN { get; set; }

        public int? idStudent { get; set; }
        public int? AppID { get; set; }
    }
}
