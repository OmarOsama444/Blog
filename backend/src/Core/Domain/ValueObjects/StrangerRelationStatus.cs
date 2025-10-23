namespace Domain.ValueObjects
{
    [Flags]
    public enum StrangerRelationStatus
    {
        None = 0,
        Anonymos = 1 << 0,
        FriendOfFriend = 1 << 1,
        Follower = 1 << 2
    }
}