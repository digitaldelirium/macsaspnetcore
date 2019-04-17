using System.Threading.Tasks;

namespace macsaspnetcore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
