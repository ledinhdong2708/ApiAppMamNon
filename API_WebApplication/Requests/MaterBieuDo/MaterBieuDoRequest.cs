namespace API_WebApplication.Requests.MaterBieuDo
{
    public class MaterBieuDoRequest
    {
        public DateTime? DocDate { get; set; }

        public int? ChieuCao { get; set; }

        public decimal? CanNang { get; set; }

        public string? BMI { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? StudentId { get; set; }
        public int? AppID { get; set; }

    }

    public class MaterBieuDoResult
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }

        public DateTime? DocDate { get; set; }

        public decimal? ChieuCao { get; set; }

        public decimal? CanNang { get; set; }

        public string? BMI { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }

        public int? StudentId { get; set; }

        public int? AppID { get; set; }
    }

}
