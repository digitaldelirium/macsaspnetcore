using System.Threading.Tasks;

namespace MacsASPNETCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
