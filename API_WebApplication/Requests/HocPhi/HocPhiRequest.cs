namespace API_WebApplication.Requests.HocPhi
{
    public class HocPhiRequest
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public int StudentId { get; set; }

        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal TotalMax { get; set; }

        public decimal TotalMin { get; set; }
        public int? AppID { get; set; }
    }
}
