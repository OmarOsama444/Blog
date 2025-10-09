using Domain.Entities;

namespace Application.Interfaces;

public interface IGroupService
{
    Task<Guid> CreateGroup(Guid creatorId, string groupName, CancellationToken token = default);
    Task AddUserToGroup(Guid groupId, ICollection<Guid> userIds, CancellationToken token);
    Task RemoveUserFromGroup(Guid groupId, Guid userId, CancellationToken token);
    Task<ICollection<Group>> GetUserGroups(Guid userId);
}
