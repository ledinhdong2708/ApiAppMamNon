using API_WebApplication.Helpers;
using API_WebApplication.Interfaces.SMS;
using API_WebApplication.Models;
using API_WebApplication.Requests.SendEmail;
using API_WebApplication.Responses.SendEmail;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace API_WebApplication.Services.SMS
{
    public class SenSMSSerivce : ISendSMSService
    {
        private readonly TwiloSettings _twilio;
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private CheckPhoneNumber _phoneNumberHelper;
        public SenSMSSerivce(IOptions<TwiloSettings> twilio, API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
            _twilio = twilio.Value;
            _phoneNumberHelper = new CheckPhoneNumber();
        }
        public async Task<MessageResource> sendSMSDD( string classID, string khoahocID)
        {

            TwilioClient.Init(_twilio.AccountSDI, _twilio.AuthToken);
            MessageResource result = null;
            var diemdanh = await _aPI_Application_V1Context.DiemDanhModels
                .Where(o => o.DenLop != true && o.CreateDate.Date == DateTime.Now.Date)
                .ToListAsync();

            foreach (var d in diemdanh)
            {
                var phuhuynh = await _aPI_Application_V1Context.Users.Where(o => o.StudentId == d.StudentId)
                    .Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                    .Where(c => (c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID)
                        || (c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID)
                        || (c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID))
                    .ToListAsync();

                foreach (var user in phuhuynh)
                {
                    string content = d.CoPhep == true ? "Học Sinh Nghỉ Có Phép" : "Học Sinh Nghỉ Học Không phép";
                    if (user.p.Phone != null)
                    {
                        string formattedPhoneNumber = _phoneNumberHelper.ValidateAndFormatPhoneNumber(user.p.Phone);
                        if (formattedPhoneNumber != "")
                        {
                            result = await MessageResource.CreateAsync(
                            body: content,
                            from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                            to: formattedPhoneNumber
                        );
                        }
                    }
                   
                }
            }

            return result;
        }

        public async Task<MessageResource> sendSMSHocPhiKhoa(string classID, string khoahocID, string month, string body)
        {
            TwilioClient.Init(_twilio.AccountSDI, _twilio.AuthToken);
            MessageResource result = null;
            var student = await _aPI_Application_V1Context.Students
            .Where(o => (o.Class1 == classID && o.Year1 == khoahocID)
            || (o.Class2 == classID && o.Year2 == khoahocID)
            || (o.Class3 == classID && o.Year3 == khoahocID))
            .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c }).ToListAsync();

            foreach (var user in student)
            {
                var hocphi = await _aPI_Application_V1Context.HocPhiModels.Where(o => o.StudentId == user.c.StudentId && o.Months == month && o.Years == DateTime.Now.Year.ToString() && o.Status == false).ToListAsync();
                foreach (var hocphimonth in hocphi)
                {
                    if (user.c.Phone != null)
                    {
                        string formattedPhoneNumber = _phoneNumberHelper.ValidateAndFormatPhoneNumber(user.c.Phone);
                        if (formattedPhoneNumber != "")
                        {
                            string messageBody = $"Nội dung:{body} \n" +
                                        $"Tháng {hocphimonth.Months}/Năm {hocphimonth.Years} \n" +
                                        $"Tổng số tiền: {hocphimonth.Total} \n" +
                                        $"Số buổi nghỉ học: {hocphimonth.Sobuoioff} \n" +
                                        $"Giá tiền nghỉ học: {hocphimonth.PhiOff} \n" +
                                        $"Số Tiền phải đóng: {hocphimonth.ConLai}";

                            result = await MessageResource.CreateAsync(
                            body: messageBody,
                            from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                            to: formattedPhoneNumber
                        );
                        }
                    }

                }
            }
         return result;
        }

        public async Task<MessageResource> sendSMSTBhocphiAll(string month, string body)
        {
            TwilioClient.Init(_twilio.AccountSDI, _twilio.AuthToken);
            MessageResource result = null;
            var student = await _aPI_Application_V1Context.Students.Where(c => c.Id > 0)
                               .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c })
                               .ToListAsync();
            foreach (var user in student)
            {
                var hocphi = await _aPI_Application_V1Context.HocPhiModels.Where(o => o.StudentId == user.c.StudentId && o.Months == month && o.Years == DateTime.Now.Year.ToString() && o.Status == false).ToListAsync();
                foreach (var hocphimonth in hocphi)
                {
                    if (user.c.Phone != null)
                    {

                        string formattedPhoneNumber = _phoneNumberHelper.ValidateAndFormatPhoneNumber(user.c.Phone);
                        if (formattedPhoneNumber != "")
                        {
                            string messageBody = $"Nội dung:{body} \n" +
                                        $"Tháng {hocphimonth.Months}/Năm {hocphimonth.Years} \n" +
                                        $"Tổng số tiền: {hocphimonth.Total} \n" +
                                        $"Số buổi nghỉ học: {hocphimonth.Sobuoioff} \n" +
                                        $"Giá tiền nghỉ học: {hocphimonth.PhiOff} \n" +
                                        $"Số Tiền phải đóng: {hocphimonth.ConLai}";

                            result = await MessageResource.CreateAsync(
                            body: messageBody,
                            from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                            to: formattedPhoneNumber
                        );
                        }
                    }

                }
            }
            return result;
        }

        public async Task<MessageResource> sendSMSTBNH(string body)
        {
            TwilioClient.Init(_twilio.AccountSDI, _twilio.AuthToken);
            MessageResource result = null;
            var student = await _aPI_Application_V1Context.Students.Where(c => c.Id > 0)
                .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c })
                .ToListAsync();
            foreach (var user in student)
            {
                if (user.c.Phone != null)
                {
                    string formattedPhoneNumber = _phoneNumberHelper.ValidateAndFormatPhoneNumber(user.c.Phone);
                    if (formattedPhoneNumber != "")
                    {
                        result = await MessageResource.CreateAsync(
                        body: body,
                        from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                        to: formattedPhoneNumber
                    );
                    }
                }
            }
            return result;
        }
    }
}
