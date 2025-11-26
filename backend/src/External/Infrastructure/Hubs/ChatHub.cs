using System.Threading.Tasks;
using Application.Interfaces;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub(IRelayService relayService) : Hub
    {
        public override async Task OnConnectedAsync()
        {

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }
        public async Task NotifyMessageReceived(Guid messageId)
        {
            var userId = Guid.TryParse(Context.UserIdentifier, out var id) ? id : throw new Exception("Invalid User Identifier");
            await relayService.RelayMessageAsync(userId, messageId, ChatMessageStatus.Delivered);
        }
        public async Task NotifyMessageSeen(Guid messageId)
        {
            var userId = Guid.TryParse(Context.UserIdentifier, out var id) ? id : throw new Exception("Invalid User Identifier");
            await relayService.RelayMessageAsync(userId, messageId, ChatMessageStatus.Seen);
        }
    }
}