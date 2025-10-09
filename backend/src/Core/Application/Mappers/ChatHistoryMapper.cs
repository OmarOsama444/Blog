using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Mappers;

public static class ChatHistoryMapper
{
    public static ChatHistoryResponse ToChatHistoryResponse(this ChatHistory chatHistory)
    {
        return new ChatHistoryResponse
        {
            Id = chatHistory.Id,
            FromUserId = chatHistory.FromUserId,
            ToUserId = chatHistory.ToUserId,
            GroupId = chatHistory.GroupId,
            Message = chatHistory.Message
        };
    }
    public static AiChatHistoryResponse ToAiChatHistoryResponse(this AiChatHistory chatHistory)
    {
        return new AiChatHistoryResponse
        {
            Id = chatHistory.Id,
            UserId = chatHistory.UserId,
            Message = chatHistory.Message,
            IsFromAi = chatHistory.IsFromAi,
            SentAtUtc = chatHistory.SentAtUtc
        };
    }
}
