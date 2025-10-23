using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Extensions;
namespace Presentation.Hubs
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