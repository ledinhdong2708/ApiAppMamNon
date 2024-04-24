using API_WebApplication.DTO;
using API_WebApplication.Interfaces.SMS;
using API_WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_WebApplication.Controllers.SMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISendSMSService _sendSMSService;
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public SMSController(ISendSMSService sendSMSService, API_Application_V1Context aPI_Application_V1Context)
        {
            _sendSMSService = sendSMSService;
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }
        [Authorize]
        [HttpPost("SendSMSDD")]

        public async Task<IActionResult> SendTB(string classID, string khoahocID)
        {
            var result = await _sendSMSService.sendSMSDD(classID, khoahocID);

            if (result == null)
            {
                return BadRequest("Failed to send SMS.");
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpPost("sendSMSTBhocphiAll")]

        public async Task<IActionResult> sendSMSTBhocphiAll(string month, SendSMS sendSMS)
        {
            var result = await _sendSMSService.sendSMSTBhocphiAll(month, sendSMS.Body);

            if (result == null)
            {
                return BadRequest("Failed to send SMS.");
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("SendSMSTBNH")]

        public async Task<IActionResult> SendSMSTBNH(SendSMS sendSMS)
        {
            var result = await _sendSMSService.sendSMSTBNH(sendSMS.Body);

            if (result == null)
            {
                return BadRequest("Failed to send SMS.");
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("sendSMSHocPhiKhoa")]

        public async Task<IActionResult> sendSMSHocPhiKhoa(string classID, string khoahocID, string month, SendSMS sendSMS)
        {
            var result = await _sendSMSService.sendSMSHocPhiKhoa(classID, khoahocID,month, sendSMS.Body);

            if (result == null)
            {
                return BadRequest("Failed to send SMS.");
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }
    }
}
