using Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
}
