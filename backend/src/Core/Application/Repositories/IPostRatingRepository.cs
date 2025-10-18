using Domain.Entities;

namespace Application.Repositories;

public interface IPostRatingRepository
{
    Task <PostRating?> GetUserRatingForPost(Guid postId, Guid userId);
    Task Add(PostRating postRating);
}