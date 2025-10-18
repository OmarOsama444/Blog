using Application.Repositories;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories;

public class PostRatingRepository(AppDbContext context) : IPostRatingRepository
{
    public async Task<PostRating?> GetUserRatingForPost(Guid postId, Guid userId)
    {
        return await context.PostRatings
            .FindAsync(postId, userId);
    }

    public async Task Add(PostRating postRating)
    {
        await context.PostRatings.AddAsync(postRating);
    }
}