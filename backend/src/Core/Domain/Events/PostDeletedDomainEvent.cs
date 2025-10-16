using Domain.Abstractions;

namespace Domain.Events;

public class PostDeletedDomainEvent : DomainEvent
{
    public Guid PostId { get; }
    public PostDeletedDomainEvent(Guid postId)
    {
        PostId = postId;
    }
}
