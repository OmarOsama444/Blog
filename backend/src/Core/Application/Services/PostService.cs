using Application.Abstractions;
using Application.Dtos.Requests;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PostService(IEmbeddingService embeddingService, IGenericRepository<Post, Guid> postRepo, IUnitOfWork unitOfWork) : IPostService
{
    public async Task<Post> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken)
    {
        var text = PostTextData(createPostRequestDto.Title, createPostRequestDto.Content, createPostRequestDto.Tags);
        var embedd = await embeddingService.GenerateEmbedingFromText(text);
        var post = Post.Create(userId, createPostRequestDto.Slug, createPostRequestDto.Title, createPostRequestDto.Content, createPostRequestDto.Tags, embedd);
        postRepo.Add(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return post;
    }
    private string PostTextData(string Title, string Content, ICollection<string> Tags)
    {
        return $"""
        Tital : {Title}
        Content : {Content}
        Tags : {string.Join(' ', Tags)}
        """;
    }
}
