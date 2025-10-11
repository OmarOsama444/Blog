using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Domain.Events;
using Domain.Abstractions;

namespace Application.Projections
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}