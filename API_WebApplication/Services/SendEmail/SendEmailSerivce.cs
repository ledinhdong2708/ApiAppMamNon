using API_WebApplication.Interfaces.SendEmail;
using API_WebApplication.Models;
using API_WebApplication.Requests.SendEmail;
using AutoMapper.Internal;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Microsoft.EntityFrameworkCore;
using API_WebApplication.Responses.SendEmail;
using API_WebApplication.Responses.SoBeNgoans;
using System;


namespace API_WebApplication.Services.SendEmail
{
    public class SendEmailSerivce :ISendEmailService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        private readonly MailSettings _mailSettings;
        public SendEmailSerivce(IOptions<MailSettings> mailSettings , API_Application_V1Context aPI_Application_V1Context)
        {
            _mailSettings = mailSettings.Value;
            this._aPI_Application_V1Context = aPI_Application_V1Context;

        }

        public async Task<SendEmailReponse> SendEmailAsync(string classID, string khoahocID )
        {

            try
            {
                var diemdanh = await _aPI_Application_V1Context.DiemDanhModels
                             .Where(o => o.DenLop != true && o.CreateDate.Date == DateTime.Now.Date)
                             .ToListAsync();


                foreach (var d in diemdanh) {
                    var phuhuynh = await _aPI_Application_V1Context.Users.Where(o => o.StudentId == d.StudentId)
                        .Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                        .Where(c => (c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID) 
                        || (c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID )
                        ||( c.c.Class1 == classID.ToString() && c.c.Year1 == khoahocID))
                        .ToListAsync();
                    foreach (var user in phuhuynh)
                    {
                        string content = d.CoPhep == true ? "Học Sinh Nghỉ Có Phép" : "Học Sinh Nghỉ Học Không phép";
                        var email = new EmailModel
                        {
                            To = user.p.Email,
                            Subject = "Điểm Danh",
                            Body = $"Nội dung: {content} <br> Ngày: {DateTime.Now} <br>  Họ Và Tên: {user.c.NameStudent}",
                        };
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                        message.To.Add(MailboxAddress.Parse(email.To));
                        var bodyBuilder = new BodyBuilder();

                        string directory = Directory.GetCurrentDirectory();
                        string relativePath = @"Helpers\email_template.html";
                        string fullPath = Path.GetFullPath(Path.Combine(directory, relativePath));
                        string htmlBody = File.ReadAllText(fullPath);



                        htmlBody = htmlBody.Replace("{{subject}}", email.Subject);
                        htmlBody = htmlBody.Replace("{{body}}", email.Body);

                        bodyBuilder.HtmlBody = htmlBody;
                        message.Body = bodyBuilder.ToMessageBody();

                        using (var client = new SmtpClient())
                        {
                            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                            await client.SendAsync(message);
                            await client.DisconnectAsync(true);
                        }
                    }

                }
                return new SendEmailReponse
                {
                    Success = true,
                };

            }
            catch (Exception ex)
            {
                return new SendEmailReponse
                {
                    Success = false,
                    Error = "Error Send Email",
                    ErrorCode = "T02"
                };
            }

        }

        public async Task<SendEmailReponse> SendEmailHocPhi(string classID, string khoahocID ,string month, MailRequest mail)
        {
            try
            { 
                    var student = await _aPI_Application_V1Context.Students
                    .Where(o => (o.Class1 == classID && o.Year1 == khoahocID )
                    || (o.Class2 == classID && o.Year2 == khoahocID)
                    || (o.Class3 == classID && o.Year3==khoahocID))
                    .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c }).ToListAsync();

                    foreach (var user in student)
                    {
                        var hocphi  = await _aPI_Application_V1Context.HocPhiModels.Where(o => o.StudentId == user.c.StudentId && o.Months == month && o.Years == DateTime.Now.Year.ToString() && o.Status == false).ToListAsync();
                       foreach(var hocphimonth in hocphi)
                        {
                        var email = new EmailModel
                        {
                            To = user.c.Email,
                            Subject = mail.Subject,
                            Body = $"Nội dung:{mail.body} <br> Tháng {hocphimonth.Months}/Năm {hocphimonth.Years} <br> Tổng số tiền:{hocphimonth.Total} <br> Số buổi nghỉ học : {hocphimonth.Sobuoioff} <br> Giá tiền nghỉ học :{hocphimonth.PhiOff} <br> Số Tiền phải đóng : {hocphimonth.ConLai}",
                        };
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                        message.To.Add(MailboxAddress.Parse(email.To));
                        var bodyBuilder = new BodyBuilder();

                        string directory = Directory.GetCurrentDirectory();
                        string relativePath = @"Helpers\email_template.html";
                        string fullPath = Path.GetFullPath(Path.Combine(directory, relativePath));
                        string htmlBody = File.ReadAllText(fullPath);



                        htmlBody = htmlBody.Replace("{{subject}}", email.Subject);
                        htmlBody = htmlBody.Replace("{{body}}", email.Body);

                        bodyBuilder.HtmlBody = htmlBody;
                        message.Body = bodyBuilder.ToMessageBody();

                        using (var client = new SmtpClient())
                        {
                            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                            await client.SendAsync(message);
                            await client.DisconnectAsync(true);
                        }
                    }
                      
                    }
                        if (student == null)
                        {
                            return new SendEmailReponse
                            {
                                Success = false,
                                Error = "Error Send Email",
                                ErrorCode = "T02"
                            };

                        }

                        return new SendEmailReponse
                        {
                            Success = true,
                    
                        };

            }
            catch (Exception ex)
            {
                return new SendEmailReponse
                {
                    Success = false,
                    Error = "Error Send Email",
                    ErrorCode = "T02"
                };
            }

        }


        public async Task<SendEmailReponse> SendEmailHocPhiALL( string month, MailRequest mail)
        {
            try
            {
                var student = await _aPI_Application_V1Context.Students.Where(c => c.Id > 0)
                    .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c })
                    .ToListAsync();
                foreach (var user in student)
                {
                    var hocphi = await _aPI_Application_V1Context.HocPhiModels.Where(o => o.StudentId == user.c.StudentId && o.Months == month && o.Years == DateTime.Now.Year.ToString() && o.Status == false).ToListAsync();
                    foreach (var hocphimonth in hocphi)
                    {
                        var email = new EmailModel
                        {
                            To = user.c.Email,
                            Subject = mail.Subject,
                            Body = $"Nội dung:{mail.body} <br> Tháng {hocphimonth.Months}/Năm {hocphimonth.Years} <br> Tổng số tiền:{hocphimonth.Total} <br> Số buổi nghỉ học : {hocphimonth.Sobuoioff} <br> Giá tiền nghỉ học :{hocphimonth.PhiOff} <br> Số Tiền phải đóng : {hocphimonth.ConLai}",
                        };
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                        message.To.Add(MailboxAddress.Parse(email.To));
                        var bodyBuilder = new BodyBuilder();

                        string directory = Directory.GetCurrentDirectory();
                        string relativePath = @"Helpers\email_template.html";
                        string fullPath = Path.GetFullPath(Path.Combine(directory, relativePath));
                        string htmlBody = File.ReadAllText(fullPath);



                        htmlBody = htmlBody.Replace("{{subject}}", email.Subject);
                        htmlBody = htmlBody.Replace("{{body}}", email.Body);

                        bodyBuilder.HtmlBody = htmlBody;
                        message.Body = bodyBuilder.ToMessageBody();

                        using (var client = new SmtpClient())
                        {
                            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                            await client.SendAsync(message);
                            await client.DisconnectAsync(true);
                        }
                    }

                }
                if (student == null)
                {
                    return new SendEmailReponse
                    {
                        Success = false,
                        Error = "Error Send Email",
                        ErrorCode = "T02"
                    };

                }

                return new SendEmailReponse
                {
                    Success = true,

                };

            }
            catch (Exception ex)
            {
                return new SendEmailReponse
                {
                    Success = false,
                    Error = "Error Send Email",
                    ErrorCode = "T02"
                };
            }

        }

        public async Task<SendEmailReponse> SendEmailTB(MailRequest mailRequest)
        {
            try
            {
                var student = await _aPI_Application_V1Context.Students.Where(c => c.Id > 0)
                    .Join(_aPI_Application_V1Context.Users, p => p.Id, c => c.StudentId, (p, c) => new { p, c })
                    .ToListAsync();
                foreach (var user in student)
                {
                        var email = new EmailModel
                        {
                            To = user.c.Email,
                            Subject = mailRequest.Subject,
                            Body = mailRequest.body,
                        };
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                    message.To.Add(MailboxAddress.Parse(email.To));
                    var bodyBuilder = new BodyBuilder();

                    string directory = Directory.GetCurrentDirectory();
                    string relativePath = @"Helpers\email_template.html";
                    string fullPath = Path.GetFullPath(Path.Combine(directory, relativePath));
                    string htmlBody = File.ReadAllText(fullPath);



                    htmlBody = htmlBody.Replace("{{subject}}", email.Subject);
                    htmlBody = htmlBody.Replace("{{body}}", email.Body);

                    bodyBuilder.HtmlBody = htmlBody;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                }

                if (student == null)
                {
                    return new SendEmailReponse
                    {
                        Success = false,
                        Error = "Error Send Email",
                        ErrorCode = "T02"
                    };

                }

                return new SendEmailReponse
                {
                    Success = true,

                };

            }
            catch (Exception ex)
            {
                return new SendEmailReponse
                {
                    Success = false,
                    Error = "Error Send Email",
                    ErrorCode = "T02"
                };
            }
        }
    }
}
