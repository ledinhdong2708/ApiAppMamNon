namespace API_WebApplication.Requests.HocPhiModel
{
    public class HocPhiChiTietModelRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public int HocPhiId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? Content { get; set; }

        public int? AppID { get; set; }

        public int MaterHocPhiId { get; set; }

        public decimal Total { get; set; }
        public int Quantity { get; set; }
    }
}
