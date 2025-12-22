using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Events;

namespace Application.Projections
{
    public class CommentCreatedDomainEventHandler : IDomainEventHandler<CommentCreatedDomainEvent>
    {
        public Task HandleAsync(CommentCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}