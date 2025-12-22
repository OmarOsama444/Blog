using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Infrastructure.Hubs
{
    [Authorize]
    public class OnlineHub() : Hub
    {
        public override async Task OnConnectedAsync()
        {

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }
    }
}