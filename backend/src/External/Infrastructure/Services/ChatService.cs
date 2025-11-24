using Application.Abstractions;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Hubs;

namespace Infrastructure.Services;

public class ChatService(IGenericRepository<Chat, Guid> chatRepo, IUnitOfWork unitOfWork, ChatHub chatHub) : IChatService
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
                // status seen for the sender
                userChatMessage.Status = UserChatMessageStatus.Seen;
            }
            chatMessage.UserChatMessageStatus.Add(userChatMessage);
        }
        chat.ChatMessages.Add(chatMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await chatHub.NotifyMessageReceived(userId, chatId, chatMessage.Id, message);
    }

}
