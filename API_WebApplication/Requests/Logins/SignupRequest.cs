using System.ComponentModel.DataAnnotations;

namespace API_WebApplication.Requests
{
    public class SignupRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required]
        public int Role { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Required]
        public DateTime Ts { get; set; }
        public int Active { get; set; }
        public int? AppID { get; set; }
    }
}
