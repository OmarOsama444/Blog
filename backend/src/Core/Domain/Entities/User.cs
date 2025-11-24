using Domain.Abstractions;
using Domain.Events;

namespace Domain.Entities;

public class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityProviderId { get; set; } = string.Empty;
    public bool TwoFactorEnabled { get; set; } = false;
    public bool IsVerified { get; set; } = false;
    public virtual ICollection<Post> Posts { get; set; } = [];
    public virtual ICollection<Comment> Comments { get; set; } = [];
    public virtual ICollection<UserRelation> FromUserRelations { get; set; } = [];
    public virtual ICollection<UserRelation> ToUserRelations { get; set; } = [];
    public virtual ICollection<ChatUser> ChatUsers { get; set; } = [];
    public virtual ICollection<ChatMessage> SentMessages { get; set; } = [];
    public virtual ICollection<ChatMessageSeen> SeenMessages { get; set; } = [];
    public virtual Profile Profile { get; set; } = default!;
    public static User Create(string email, string firstName, string lastName, string identityProviderId)
    {
        var user = new User
        {
            Id = Guid.Parse(identityProviderId),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityProviderId = identityProviderId,
            TwoFactorEnabled = false,
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}
