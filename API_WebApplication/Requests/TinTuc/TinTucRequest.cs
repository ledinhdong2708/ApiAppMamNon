namespace API_WebApplication.Requests.TinTuc
{
    public class TinTucRequest
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }
    }
}
