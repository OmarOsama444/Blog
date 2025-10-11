using Application.Abstractions;
using Application.Dtos.Requests;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PostService(IGenericRepository<Post, Guid> postRepo, IUnitOfWork unitOfWork) : IPostService
{
    public async Task<Post> CreatePostAsync(Guid userId, CreatePostRequestDto createPostRequestDto, CancellationToken cancellationToken)
    {
        var post = Post.Create(userId, createPostRequestDto.Slug, createPostRequestDto.Title, createPostRequestDto.Content, createPostRequestDto.Tags);
        postRepo.Add(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return post;
    }
}
