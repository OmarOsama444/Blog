using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Profile : Entity
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; } = default!;
        public StrangerRelationStatus CanRequestFriend { get; set; } = StrangerRelationStatus.Anonymos;
        public StrangerRelationStatus AutoAcceptFriend { get; set; } = StrangerRelationStatus.None;
        public StrangerRelationStatus CanRequestChat { get; set; } = StrangerRelationStatus.Anonymos;
        public StrangerRelationStatus AutoAcceptChat { get; set; } = StrangerRelationStatus.None;
    }
}