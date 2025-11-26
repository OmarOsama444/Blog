using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderUserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentOnUtc { get; set; }
        public DateTime LastStatusUpdateOnUtc { get; set; } = DateTime.UtcNow;
        public ChatMessageStatus Status { get; set; } = ChatMessageStatus.Sent;
        public virtual User User { get; set; } = null!;
        public virtual Chat Chat { get; set; } = null!;
        public virtual ICollection<UserChatMessage> UserChatMessages { get; set; } = [];
        public bool UpdateStatus(ChatMessageStatus newStatus)
        {
            if (newStatus > Status)
            {
                Status = newStatus;
                LastStatusUpdateOnUtc = DateTime.UtcNow;
                return true;
            }
            return false;
        }
    }
}