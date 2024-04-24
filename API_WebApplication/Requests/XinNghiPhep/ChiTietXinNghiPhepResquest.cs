namespace API_WebApplication.Requests.XinNghiPhep
{
    public class ChiTietXinNghiPhepResquest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public int XinNghiPhepId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? Content { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public int? AppID { get; set; }
    }
}
