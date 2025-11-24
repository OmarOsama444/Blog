using Domain.ValueObjects;

namespace Domain.Entities
{
    public class UserChatMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatMessageId { get; set; }
        public Guid UserId { get; set; }
        public DateTime LastStatusUpdateOnUtc { get; set; } = DateTime.UtcNow;
        public UserChatMessageStatus Status { get; set; } = UserChatMessageStatus.Sent;
        public virtual ChatMessage ChatMessage { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public void UpdateStatus(UserChatMessageStatus newStatus)
        {
            if (newStatus > Status)
            {
                Status = newStatus;
                LastStatusUpdateOnUtc = DateTime.UtcNow;
            }
        }
    }
}