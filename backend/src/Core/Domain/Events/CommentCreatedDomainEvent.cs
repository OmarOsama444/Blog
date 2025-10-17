using Domain.Abstractions;

namespace Domain.Events;

public class CommentCreatedDomainEvent : DomainEvent
{
    public string CommentId { get; }

    public CommentCreatedDomainEvent(string commentId)
    {
        CommentId = commentId;
    }
}
