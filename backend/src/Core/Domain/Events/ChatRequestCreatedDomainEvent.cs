using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Domain.Events
{
    public class ChatRequestCreatedDomainEvent(Guid ChatRequestId) : DomainEvent
    {
        public Guid ChatRequestId { get; set; } = ChatRequestId;
    }
}