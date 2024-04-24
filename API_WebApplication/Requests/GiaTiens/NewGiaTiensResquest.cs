namespace API_WebApplication.Requests.GiaTiens
{
    public class NewGiaTiensResquest
    {
        public int id { get; set; }
        public string name { get; set; }

        public decimal gia { get; set; }

        public DateTime CreateDate {  get; set; }
        
        public DateTime UpdateDate { get; set; }
    }
}
