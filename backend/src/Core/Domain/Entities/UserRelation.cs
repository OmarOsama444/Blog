using Domain.ValueObjects;

namespace Domain.Entities
{
    public class UserRelation
    {
        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public User FromUser { get; set; } = default!;
        public User ToUser { get; set; } = default!;
        public RelationType Relation { get; set; }
        public StatusType Status { get; set; }
    }
}