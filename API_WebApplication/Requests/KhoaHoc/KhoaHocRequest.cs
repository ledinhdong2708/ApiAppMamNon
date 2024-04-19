namespace API_WebApplication.Requests.KhoaHoc
{
    public class KhoaHocRequest
    {
        public int? FromYear { get; set; }

        public int? ToYear { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }
    }
}
