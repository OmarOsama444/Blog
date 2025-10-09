using Domain.Abstractions;

namespace Domain.Events;

public class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; }

    public UserCreatedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
}
