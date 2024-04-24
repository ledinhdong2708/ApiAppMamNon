using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using API_WebApplication.Interfaces.PhanCongGiaoVienModels;
using API_WebApplication.Interfaces.SendEmail;
using API_WebApplication.Models;
using MailKit;
using API_WebApplication.Services.SendEmail;
using MimeKit;
using MimeKit.Text;
using AutoMapper.Internal;
using API_WebApplication.Requests.SendEmail;
using Microsoft.AspNetCore.Authorization;
using System;

namespace API_WebApplication.Controllers.SendEmail
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmailService _sendEmailService;
        public SendEmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }
        [Authorize]
        [HttpPost("sendDiemDanh")]
        public async Task<IActionResult> SendEmailDB(string classID , string khoahocID)
        {
            try
            {
                var sendResponse = await _sendEmailService.SendEmailAsync(classID,khoahocID);
                if (!sendResponse.Success )
                {
                    return UnprocessableEntity(sendResponse);
                }
                return Ok(sendResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpPost("sendTBhocphiKhoa")]
            public async Task<IActionResult> SendEmailHocPhi( string classID , string khoahocID , string month, MailRequest emailModel)
            {
                try
                {
                    var sendResponse = await _sendEmailService.SendEmailHocPhi( classID, khoahocID , month, emailModel);
                   if (!sendResponse.Success )
                    {
                        return UnprocessableEntity(sendResponse);
                    }
                    return Ok(sendResponse);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        [Authorize]
        [HttpPost("sendTBhocphiAll")]
        public async Task<IActionResult> SendEmailHocPhiAll(string month ,MailRequest mailRequest)
        {
            try
            {
                var sendResponse = await _sendEmailService.SendEmailHocPhiALL(month,mailRequest);
                if (!sendResponse.Success)
                {
                    return UnprocessableEntity(sendResponse);
                }
                return Ok(sendResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("sendTB")]
        public async Task<IActionResult> SendEmailTB(MailRequest mailRequest)
        {
            try
            {
                var sendResponse = await _sendEmailService.SendEmailTB( mailRequest);
                if (!sendResponse.Success)
                {
                    return UnprocessableEntity(sendResponse);
                }
                return Ok(sendResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
