namespace API_WebApplication.Requests.GiaTiens
{
    public class GiaTiensResquest
    {
        public int Id { get; set; }
        public string name { get; set; }

        public decimal gia { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
