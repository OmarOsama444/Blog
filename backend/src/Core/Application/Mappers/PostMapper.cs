using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Mappers
{
    public static class PostMapper
    {
        public static PostResponseDto ToPostResponseDto(this Post post)
        {
            return new PostResponseDto
            {
                Id = post.Id,
                Slug = post.Slug,
                Rating = post.Rating,
                TotalRatings = post.TotalUsersRated,
                Title = post.Title,
                CreatedOnUtc = post.CreatedOnUtc,
                Tags = post.Tags
            };
        }
    }
}