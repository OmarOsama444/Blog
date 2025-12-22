using Domain.ValueObjects;

namespace Domain.Entities
{
    public class UserRelation
    {
        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public virtual User FromUser { get; set; } = default!;
        public virtual User ToUser { get; set; } = default!;
        public RelationType Relation { get; set; }
        public StatusType Status { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public static UserRelation Create(Guid FromId, Guid ToId, RelationType relationType, StatusType statusType)
        {
            return new UserRelation
            {
                FromId = FromId,
                ToId = ToId,
                Relation = relationType,
                Status = statusType,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}