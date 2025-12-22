using Domain.Abstractions;
using Domain.Events;

namespace Domain.Entities;

public class Comment : Entity
{
    public string Id { get; set; } = string.Empty;
    public string? ParentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public Guid PostId { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public virtual User User { get; set; } = default!;
    public virtual Post Post { get; set; } = default!;
    public virtual Comment? Parent { get; set; }
    public static Comment Create(string Content, User user, Guid PostId, string? ParentId = null)
    {

        var comment = new Comment
        {
            Id = Ulid.NewUlid().ToString(),
            Content = Content,
            UserId = user.Id,
            UserFullName = user.FirstName + " " + user.LastName,
            PostId = PostId,
            ParentId = ParentId
        };
        comment.RaiseDomainEvent(new CommentCreatedDomainEvent(comment.Id));
        return comment;
    }
}