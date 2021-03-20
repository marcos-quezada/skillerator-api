using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;

namespace Skillerator.Services{
    public class ZohoMailEmailSender : IEmailSender{
        
        private readonly IConfiguration _config;
        public ZohoMailEmailSender(IConfiguration config)
        {
            _config = config;
        }        
        public void SendEmail(string to, string title, string body){
            using (var client = new SmtpClient("smtppro.zoho.eu", 587)){
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;

                var username = _config["SMTP-username"];
                var password = _config["SMTP-password"];

                client.Credentials = new NetworkCredential(username, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(username);
                mailMessage.To.Add(to);
                mailMessage.Subject = title;
                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
            }
        }
    }
}