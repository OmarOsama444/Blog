using Domain.Abstractions;
using Domain.Events;

namespace Domain.Entities;

public class PostRating : Entity
{
    public required Guid UserId { get; set; }
    public required Guid PostId { get; set; }
    public required byte Rating { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;

    public static PostRating Create(Guid userId, Guid postId, byte rate)
    {
        var userPostRate = new PostRating
        {
            UserId = userId,
            PostId = postId,
            Rating = rate,
            CreatedOnUtc = DateTime.UtcNow
        };
        
        userPostRate.RaiseDomainEvent(new PostRatingDomainEvent(userPostRate.PostId, userPostRate.Rating));
        
        return userPostRate;
    }
    
    public void Update(byte newRate)
    {
        var oldRate = Rating;
        Rating = newRate;
        
        RaiseDomainEvent(new PostRatingDomainEvent(PostId, Rating, true, oldRate));
    }
}