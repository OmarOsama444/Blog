using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Presentation.Extensions;
namespace Presentation.Hubs
{
    [Authorize]
    public class ChatHub() : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext()!;
            var userId = httpContext.User.GetUserId();
            string reciveUserId = httpContext?.Request.Query["userId"]!;
            await Groups.AddToGroupAsync(Context.ConnectionId, reciveUserId);
            await Clients.Group(reciveUserId).SendAsync("User Open Chat", userId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext()!;
            var userId = httpContext.User;
            string chatId = httpContext?.Request.Query["chatId"]!;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("User Left Chat", userId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}