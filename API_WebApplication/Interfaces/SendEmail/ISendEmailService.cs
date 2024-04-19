using API_WebApplication.Models;
using API_WebApplication.Requests.SendEmail;
using API_WebApplication.Responses.SendEmail;
using AutoMapper.Internal;

namespace API_WebApplication.Interfaces.SendEmail
{
    public interface ISendEmailService
    {
        Task<SendEmailReponse> SendEmailAsync(string classID , string khoahocID);

        Task<SendEmailReponse> SendEmailHocPhi( string classID , string khoahocID,string month, MailRequest email );
        Task<SendEmailReponse> SendEmailHocPhiALL(string month, MailRequest email);
        Task<SendEmailReponse> SendEmailTB( MailRequest email);
    }
}
