using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces;

public interface IELasticService
{
    public Task CreatePostAsync(Post post, CancellationToken cancellationToken = default);
    public Task<ICollection<PostResponseDto>> SearchPostSemantic(SearchPostRequestDto requestDto);
}
