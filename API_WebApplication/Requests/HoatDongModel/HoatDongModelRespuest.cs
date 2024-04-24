namespace API_WebApplication.Requests.HoatDongModel
{
    public class HoatDongModelRespuest
    {
       
            public int Id { get; set; }

            public int UserId { get; set; }

            public int Role { get; set; }

            public string? Content { get; set; }

            public DateTime? CreateDate { get; set; }

            public DateTime? UpdateDate { get; set; }

            public bool IsCompleted { get; set; }

            public int? AppID { get; set; }
            public string? Img { get; set; }
            public int? ClassID { get; set; }
            public string? Months { get; set; }
           public string? Years { get; set; }
        public int? KhoaHocId { get; set; }
    }
}
