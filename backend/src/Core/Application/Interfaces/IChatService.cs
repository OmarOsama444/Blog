using Application.Dtos.Responses;

namespace Application.Interfaces;

public interface IChatService
{
    public Task SendMessageAsync(Guid userId, Guid chatId, string message, CancellationToken cancellationToken = default);
}
