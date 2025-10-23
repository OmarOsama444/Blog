using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IRelationService
    {
        public Task CreateRelation(Guid FromId, Guid ToId, RelationType relationType);
        public Task GetByRelation(Guid UserId, RelationType relationType, StatusType statusType, bool TwoWay = false);
        public Task ApproveRelation(Guid UserId, RelationType relationType, StatusType statusType, bool TwoWay = false);
    }
}