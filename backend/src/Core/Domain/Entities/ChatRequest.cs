using Domain.Abstractions;
using Domain.Events;

namespace Domain.Entities
{
    public class ChatRequest : Entity
    {
        public Guid Id { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedOnUtc { get; set; }
        public static ChatRequest Create(Guid FromId, Guid ToId, string Message)
        {
            var cr = new ChatRequest
            {
                Id = Guid.NewGuid(),
                FromUserId = FromId,
                ToUserId = ToId,
                Message = Message,
                CreatedOnUtc = DateTime.UtcNow
            };
            cr.RaiseDomainEvent(new ChatRequestCreatedDomainEvent(cr.Id));
            return cr;
        }
    }
}