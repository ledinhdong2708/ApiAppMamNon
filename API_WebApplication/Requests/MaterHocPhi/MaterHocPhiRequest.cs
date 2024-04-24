namespace API_WebApplication.Requests.MaterChiPhi
{
    public class MaterHocPhiRequest
    {
        public string? Content { get; set; }

        public string? DonViTinh { get; set; }

        public int UserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public int? StudentId { get; set; }

        public int? AppID { get; set; }
    }
}
