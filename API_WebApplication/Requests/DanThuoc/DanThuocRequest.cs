namespace API_WebApplication.Requests.DanThuoc
{
    public class DanThuocRequest
    {
        public DateTime? DocDate { get; set; }

        public string? Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }

        public int? StudentId { get; set; }
        public int? AppID { get; set; }
    }
}
