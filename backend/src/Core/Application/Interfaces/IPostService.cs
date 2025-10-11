using Application.Dtos.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostService
{
    public Task<Post> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken = default);
}
