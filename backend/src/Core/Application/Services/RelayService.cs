using System.Net.NetworkInformation;
using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services
{
    public class RelayService(IGenericRepository<ChatMessage, Guid> ChatMessageRepo, IUnitOfWork unitOfWork) : IRelayService
    {
        public async Task<bool> RelayMessageAsync(Guid userId, Guid messageId, ChatMessageStatus messageStatus, CancellationToken cancellationToken = default)
        {
            ChatMessage? chatMessage = await ChatMessageRepo.GetById(messageId);
            if (chatMessage == null)
                return false;
            bool statusUpdated = chatMessage.UpdateStatus(messageStatus);
            if (!statusUpdated)
                return false;
            ChatMessageRepo.Update(chatMessage);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}