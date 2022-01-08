using System.Threading.Tasks;

namespace StreamCommunity.Application.Common;

public interface IChatMessaging
{
    Task SendMessageAsync(string message);
}