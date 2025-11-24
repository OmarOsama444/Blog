using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub() : Hub
    {
        public override async Task OnConnectedAsync()
        {

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }
        public async Task NotifyMessageReceived(Guid senderUserId, Guid chatId, Guid messageId, string message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageId, chatId, senderUserId, message);
        }
    }
}