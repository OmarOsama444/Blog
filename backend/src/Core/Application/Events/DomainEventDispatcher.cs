using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions;
using Domain.Abstractions;

namespace Application.Events;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

        var handlers = serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync");

            if (method is not null)
            {
                await (Task)method.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
            }
        }
    }
}
