using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UserRepository(ChatDbContext chatDbContext) : IUserRepository
    {
        public Task<User?> GetByEmail(string email)
        {
            return chatDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ICollection<User>> GetByGroupId(Guid groupId)
        {
            return await chatDbContext.GroupUsers
                .Where(gu => gu.GroupId == groupId)
                .Include(x => x.User)
                .Select(gu => gu.User)
                .ToListAsync();
        }
    }
}