using System.Threading.Tasks;
using MimeKit;

namespace MacsASPNETCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MimeMessage message);
        Task SendEmailAsync(string to, string subject, string message);
        bool SendMail(MimeMessage message);
        bool SendMail(string to, string from, string subject, string body);
    }
}
