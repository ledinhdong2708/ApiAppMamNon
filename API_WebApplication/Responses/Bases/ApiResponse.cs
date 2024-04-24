namespace API_WebApplication.Responses.Bases
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
