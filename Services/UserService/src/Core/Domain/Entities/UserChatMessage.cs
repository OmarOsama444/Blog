using Domain.ValueObjects;

namespace Domain.Entities
{
    public class UserChatMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatMessageId { get; set; }
        public Guid UserId { get; set; }
        public bool Relayed { get; set; } = false;
        public virtual ChatMessage ChatMessage { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}