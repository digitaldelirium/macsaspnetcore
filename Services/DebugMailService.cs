using System.Diagnostics;
using System.Threading.Tasks;
using MimeKit;

namespace MacsASPNETCore.Services
{
    public class DebugMailService : IEmailSender
    {
        public Task SendEmailAsync(MimeMessage message)
        {
            Debug.WriteLine($"Sending message:  To:{message.To}  Subject:{message.Subject}  Message: {message.Body}");
            return Task.FromResult(0);
        }

        public Task SendEmailAsync(string to, string subject, string message)
        {
            Debug.WriteLine($"Sending mail To:{to}, Subject: {subject}, Message: {message}");
            return Task.FromResult(0);
        }

        public bool SendMail(MimeMessage message)
        {
            Debug.WriteLine($"Sending mail: To: {message.To}, Subject: {message.Subject}, Message: {message.Body}");
            return true;
        }

        public bool SendMail(string to, string @from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail: To: {to}, Subject: {subject}, Message: {body}");
            return true;
        }
    }
}
