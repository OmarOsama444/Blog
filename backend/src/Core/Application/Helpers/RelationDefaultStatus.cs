using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Helpers
{
    public static class RelationDefaultStatusFactory
    {
        public static StatusType GetDefaultStatus(RelationType relationType)
        {
            return relationType switch
            {
                RelationType.Friend => StatusType.Pending,
                RelationType.Block => StatusType.Active,
                RelationType.Follow => StatusType.Pending,
                _ => StatusType.None,
            };
        }
    }
}