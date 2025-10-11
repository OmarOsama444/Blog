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
    public class UserRepository(BlogDbContext blogDbContext) : IUserRepository
    {
        public Task<User?> GetByEmail(string email)
        {
            return blogDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}