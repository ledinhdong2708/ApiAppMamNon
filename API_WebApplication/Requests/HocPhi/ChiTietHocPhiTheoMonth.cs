namespace API_WebApplication.Requests.HocPhi
{
    public class ChiTietHocPhiTheoMonth
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? Months { get; set; }

        public string? Years { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal Total { get; set; }

        public int HocPhiChiTietId { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }
        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }
    }
}
