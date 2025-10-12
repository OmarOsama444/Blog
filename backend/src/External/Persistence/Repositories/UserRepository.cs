using Microsoft.EntityFrameworkCore;
using Application.Repositories;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public Task<User?> GetByEmail(string email)
        {
            return context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}