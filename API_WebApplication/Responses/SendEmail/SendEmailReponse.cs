using API_WebApplication.Models;

namespace API_WebApplication.Responses.SendEmail
{
    public class SendEmailReponse : BaseResponse
    {
        public EmailModel EmailModel { get; set; }
    }
}
