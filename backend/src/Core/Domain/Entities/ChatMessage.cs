namespace Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderUserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentOnUtc { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Chat Chat { get; set; } = null!;
        public virtual ICollection<UserChatMessage> UserChatMessageStatus { get; set; } = [];
    }
}