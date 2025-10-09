using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChatService
    {
        public Task<ChatHistoryResponse> SendToUser(Guid FromId, Guid ToId, string Message, CancellationToken token = default);
        public Task<ChatHistoryResponse> SendToGroup(Guid FromId, Guid GroupId, string Message, CancellationToken token = default);
        public Task<ICollection<ChatHistoryResponse>> FetchUserChatHistory(Guid FromId, Guid ToId, int Total = 20, string? LastId = null, CancellationToken token = default);
        public Task<ICollection<ChatHistoryResponse>> FetchGroupChatHistory(Guid FromId, Guid GroupId, int Total, string? LastId = null, CancellationToken token = default);
    }
}