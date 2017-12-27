using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MacsASPNETCore.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private MimeMessage _msg;
        private readonly IConfiguration _configuration;
        
        public AuthMessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public  Task SendEmailAsync(MimeMessage msg)
        {
            // Plug in your email service here to send an email.
            // Pull API Key from Config

            var apiKey = _configuration["AppSettings:SendGridSMTPAPIKey"];
            var username = _configuration["AppSettings:SendGridUserName"];
            var password = _configuration["AppSettigns:SendGridPassword"];
            NetworkCredential credential = new NetworkCredential(username, password);

            // Create transport and send message
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ConnectAsync("smtp.sendgrid.net", Convert.ToInt32(587));
                    client.AuthenticateAsync(credential);
                    client.SendAsync(msg);
                    client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.FromResult(0);
        }

        public Task SendEmailAsync(string to, string subject, string message)
        {
            var apiKey = _configuration["AppSettings:SendGridSMTPAPIKey"];
            var username = _configuration["AppSettings:SendGridUserName"];
            var password = _configuration["AppSettigns:SendGridPassword"];
            var email = _configuration["AppSettings:InfoEmailAddress"];

            NetworkCredential credential = new NetworkCredential(username, password);
            var msg = new MimeMessage();
            msg.To.Add(new MailboxAddress("Mac's User", to));
            msg.From.Add(new MailboxAddress("Mac's Information", email));
            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            {
                Text = message
            };

            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ConnectAsync("smtp.sendgrid.net", Convert.ToInt32(587));
                    client.AuthenticateAsync(credential);
                    client.SendAsync(msg);
                    client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return Task.FromResult(0);
        }

        public bool SendMail(MimeMessage message)
        {
            _msg = message;
            if (_msg.To != null) SendEmailAsync(message);
            return true;

        }

        public bool SendMail(string to, string from, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
