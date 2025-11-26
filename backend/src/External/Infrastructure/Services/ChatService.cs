using Application.Abstractions;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services;

public class ChatService(IGenericRepository<Chat, Guid> chatRepo, IUnitOfWork unitOfWork, IHubContext<ChatHub> chatHubContext) : IChatService
{
    public async Task SendMessageAsync(Guid userId, Guid chatId, string message, CancellationToken cancellationToken = default)
    {
        Chat chat = await chatRepo.GetById(chatId) ?? throw new NotFoundException("Chat.NotFound", chatId);
        var chatUsers = chat.ChatUsers;
        // validate the user is part of the chat
        if (!chatUsers.Select(x => x.UserId).Contains(userId))
            throw new NotAuthorizedException("Chat.NotAuthorized", userId);

        // adding chat message from aggreagte root
        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid(),
            ChatId = chatId,
            SenderUserId = userId,
            Message = message,
            SentOnUtc = DateTime.UtcNow
        };

        // status messages for the users
        ICollection<UserChatMessage> userChatMessages = [.. chatUsers.Select(x => new UserChatMessage
        {
            UserId = x.UserId,
            ChatMessageId = chatMessage.Id,
        })];
        foreach (var userChatMessage in userChatMessages)
        {
            if (userChatMessage.UserId == userId)
            {
                // status relayed for the sender to true
                userChatMessage.Relayed = true;
            }
            chatMessage.UserChatMessages.Add(userChatMessage);
        }
        chat.ChatMessages.Add(chatMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await chatHubContext.Clients.Group(chatId.ToString()).SendAsync("SentMessage", userId, chatId, chatMessage.Id, message, cancellationToken: cancellationToken);
    }

}
