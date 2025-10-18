using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Events;

namespace Application.Events.Handlers;

public class PostRatingDomainEventHandler(IGenericRepository<Post, Guid> postRepo, IUnitOfWork unitOfWork, IElasticService elasticService) : IDomainEventHandler<PostRatingDomainEvent>
{
    public async Task HandleAsync(PostRatingDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var post = await postRepo.GetByIdForUpdate(domainEvent.PostId) ?? throw new Exception($"Post with ID {domainEvent.PostId} not found.");
            post.UpdateAverageRating(domainEvent.Rating, domainEvent.IsUpdate, domainEvent.OldRating);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            /*var elasticPost = await elasticService.GetPostByIdAsync(post.Id, cancellationToken);
            if (elasticPost != null)
            {
                elasticPost.Rating = post.Rating;
                elasticPost.TotalUsersRated = post.TotalUsersRated;
                await elasticService.UpsertPostAsync(elasticPost, cancellationToken);
            }*/
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
        
    }
}