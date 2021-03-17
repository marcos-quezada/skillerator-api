using Darnton.OpenWeather.Models;
using Darnton.OpenWeather.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Skillerator.Services;
using Skillerator.Models;

namespace Skillerator.Controllers
{
    [ApiController]
    [Route("api/send-email")]
    public class EmailSenderController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSenderController> _logger;
        

        public EmailSenderController(IConfiguration config, ILogger<EmailSenderController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(EmailContentData EmailData)
        {
            if (EmailData is null){
                return BadRequest("You must submit the required email data.");
            }

            if (string.IsNullOrEmpty(EmailData.to)){
                return BadRequest("You must provide at least a detination email address");
            }

            IEmailSender EmailSender = new ZohoMailEmailSender(_config);
            EmailSender.SendEmail(EmailData.to,
                    EmailData.subject,
                    EmailData.body);

            return Ok();
        }
    }
}