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
        public void Post()
        {
            IEmailSender EmailSender = new ZohoMailEmailSender(_config);
            EmailSender.SendEmail("quezad@gmail.com",
                    "You received a new file",
                    "Test");
        }
    }
}