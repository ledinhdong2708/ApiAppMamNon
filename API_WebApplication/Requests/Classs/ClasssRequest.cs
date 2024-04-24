namespace API_WebApplication.Requests.Classs
{
    public class ClasssRequest
    {
        public string? NameClass { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }
    }
}
