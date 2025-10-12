using Application.Abstractions;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class PostService(IEmbeddingService embeddingService, IGenericRepository<Post, Guid> postRepo, IPostRepositroy postRepositroy, IUnitOfWork unitOfWork) : IPostService
{
    public async Task<PostResponseDto> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken)
    {
        var text = PostTextData(createPostRequestDto.Title, createPostRequestDto.Content, createPostRequestDto.Tags);
        var embedd = await embeddingService.GenerateEmbedingFromText(text);
        var post = Post.Create(userId, createPostRequestDto.Slug, createPostRequestDto.Title, createPostRequestDto.Content, createPostRequestDto.Tags, embedd);
        postRepo.Add(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return post.ToPostResponseDto();
    }

    public async Task<ICollection<PostResponseDto>> SearchPost(SearchPostRequestDto searchPostRequestDto, CancellationToken cancellationToken)
    {
        return await
        postRepositroy.SearchByFullText(searchPostRequestDto.SearchTerm, searchPostRequestDto.Page, searchPostRequestDto.PageSize, cancellationToken);
    }

    public async Task DeletePost(Guid UserId, ICollection<Guid> PostIds, CancellationToken cancellationToken)
    {
        foreach (var postId in PostIds)
        {
            Post post = await postRepo.GetById(postId) ?? throw new NotFoundException("Post.NotFound", postId);
            if (UserId != post.UserId)
                throw new NotAuthorizedException("User.Post.NotAuhorized", UserId, postId);
            postRepo.Remove(post);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
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
