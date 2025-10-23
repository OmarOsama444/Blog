using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRelationRepository
    {
        public Task<ICollection<User>> GetByUserId(Guid userId, UserRelation relation);
    }
}