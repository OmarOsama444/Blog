using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces;

public interface IElasticService
{
    public Task UpsertPostAsync(Post post, CancellationToken cancellationToken = default);
    public Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<ICollection<PostResponseDto>> SearchPostSemantic(SearchPostRequestDto requestDto);
    public Task<ICollection<PostResponseDto>> SearchPostByTextFuzzyAsync(SearchPostRequestDto requestDto);
}
