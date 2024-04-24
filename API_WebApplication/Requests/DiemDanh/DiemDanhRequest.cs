namespace API_WebApplication.Requests.DiemDanh
{
    public class DiemDanhRequest
    {
        public int IdUser { get; set; }  
        public int StudentId { get; set; }

        public string? Content { get; set; }

        public bool DenLop { get; set; }

        public bool CoPhep { get; set; }

        public bool KhongPhep { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? AppID { get; set; }

        public int idDiemDanh { get; set;}
    }
}
