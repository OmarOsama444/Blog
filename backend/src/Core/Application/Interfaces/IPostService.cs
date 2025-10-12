using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostService
{
    public Task<PostResponseDto> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken = default);
    public Task<ICollection<PostResponseDto>> SearchPost(SearchPostRequestDto searchPostRequestDto, CancellationToken cancellationToken = default);
    public Task DeletePost(Guid UserId, ICollection<Guid> PostId, CancellationToken cancellationToken = default);
}
