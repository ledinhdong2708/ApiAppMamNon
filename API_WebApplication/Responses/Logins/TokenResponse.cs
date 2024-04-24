namespace API_WebApplication.Responses.Logins
{
    public class TokenResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string ExpiryDate { get; set; }
        public string statusCode { get; set; }
        public string userID { get; set; }
        public string studentID { get; set; }
        public string appID { get; set; }
    }
}
