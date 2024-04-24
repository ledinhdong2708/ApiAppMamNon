namespace API_WebApplication.Requests.Logins
{
    public class UserRequest
    {
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? Address2 { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public int? AppID { get; set; }
    }
}
