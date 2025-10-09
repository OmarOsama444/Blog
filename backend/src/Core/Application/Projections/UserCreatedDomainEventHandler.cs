using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Domain.Events;
using Domain.Abstractions;

namespace Application.Projections
{
    public class UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger) : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            logger.LogInformation(
                "UserCreatedDomainEventHandler: User with ID {UserId} created at {CreationTime}",
                domainEvent.UserId,
                domainEvent.CreatedOnUtc
            );
            return Task.CompletedTask;
        }
    }
}