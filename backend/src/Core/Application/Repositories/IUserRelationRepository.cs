using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Repositories
{
    public interface IUserRelationRepository
    {
        // follow uni directional 
        // friend bi directional id1 == x and id2 == y || id1 == y && id2 == X 
        // friend bi directional id1 == x and id2 == y when add x , y  y , x 
        // friend bi directional sort the ids before querying 
        // add extra flag that represents the relations edge uni directional or bidirectional
        public Task<UserRelation?> GetByFromIdAndToIdAndRelation(Guid FromId, Guid ToId, RelationType relationType);
        public Task<UserRelation?> GetByFromIdAndToIdAndRelationForUpdate(Guid FromId, Guid ToId, RelationType relationType);
        public Task<int> CountMutualFriends(Guid userA, Guid userB);
    }
}