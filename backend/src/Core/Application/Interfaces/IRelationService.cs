using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IRelationService
    {
        public Task ApproveFriendRequest(Guid ReciverId, Guid SenderId, CancellationToken cancellationToken);
        public Task SendFriendRequest(Guid FromId, Guid ToId, CancellationToken cancellationToken);
        public Task SendFollowRequest(Guid FromId, Guid ToId, CancellationToken cancellationToken);
    }
}