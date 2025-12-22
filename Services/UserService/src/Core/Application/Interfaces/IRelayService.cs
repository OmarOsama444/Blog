using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IRelayService
    {
        public Task<bool> RelayMessageAsync(Guid userId, Guid messageId, ChatMessageStatus messageStatus, CancellationToken cancellationToken = default);
    }
}