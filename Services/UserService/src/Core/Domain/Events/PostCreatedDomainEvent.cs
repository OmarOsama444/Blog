using Domain.Abstractions;

namespace Domain.Events;

public class PostCreatedDomainEvent : DomainEvent
{
    public Guid PostId { get; }
    public PostCreatedDomainEvent(Guid postId)
    {
        PostId = postId;
    }
}
