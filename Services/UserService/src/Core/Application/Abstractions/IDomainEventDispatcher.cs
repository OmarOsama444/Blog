using Domain.Abstractions;

namespace Application.Abstractions
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
    }

}