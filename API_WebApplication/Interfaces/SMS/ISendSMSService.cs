using Twilio.Rest.Api.V2010.Account;

namespace API_WebApplication.Interfaces.SMS
{
    public interface ISendSMSService
    {
        Task<MessageResource> sendSMSDD( string classID, string khoahocID);

        Task<MessageResource> sendSMSTBhocphiAll(string month, string body);

        Task<MessageResource> sendSMSTBNH(string body);


        Task<MessageResource> sendSMSHocPhiKhoa(string classID, string khoahocID, string month ,string body);
    }
}
