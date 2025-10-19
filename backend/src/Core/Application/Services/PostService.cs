using Application.Abstractions;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class PostService(IEmbeddingService embeddingService, IGenericRepository<Post, Guid> postRepo, IPostRepositroy postRepositroy, IPostRatingRepository postRatingRepository, IUnitOfWork unitOfWork) : IPostService
{
    public async Task<PostResponseDto> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken)
    {
        var existingPost = await postRepositroy.GetBySlug(createPostRequestDto.Slug, cancellationToken);
        if (existingPost is not null)
            throw new ConflictException("Post.Slug.Conflict", createPostRequestDto.Slug);
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

    public async Task RatePost(Guid userId, Guid postId, RatePostRequestDto ratePostRequestDto)
    {
        _ = await postRepo.GetById(postId) ?? throw new NotFoundException("Post.NotFound", postId);
        
        var existingPostRating = await postRatingRepository.GetUserRatingForPost(postId, userId);
        if (existingPostRating is not null)
            existingPostRating.Update(ratePostRequestDto.Rating);

        else
        {
            var postRating = PostRating.Create(userId, postId, ratePostRequestDto.Rating);
            await postRatingRepository.Add(postRating);
        }
        
        await unitOfWork.SaveChangesAsync();
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
