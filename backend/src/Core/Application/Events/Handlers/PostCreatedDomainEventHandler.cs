using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Events;

namespace Application.Events.Handlers
{
    public class PostCreatedDomainEventHandler(IGenericRepository<Post, Guid> postRepo, IELasticService eLasticService) : IDomainEventHandler<PostCreatedDomainEvent>
    {
        public async Task HandleAsync(PostCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var post = await postRepo.GetById(domainEvent.PostId) ?? throw new Exception($"Post with ID {domainEvent.PostId} not found.");
            await eLasticService.CreatePostAsync(post, cancellationToken);
        }
    }
}