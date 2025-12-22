namespace Domain.Entities
{
    public class ChatUser
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedOnUtc { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public virtual Chat Chat { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}