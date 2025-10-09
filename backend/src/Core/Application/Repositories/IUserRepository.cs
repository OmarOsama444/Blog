using Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
    public Task<ICollection<User>> GetByGroupId(Guid groupId);
}
