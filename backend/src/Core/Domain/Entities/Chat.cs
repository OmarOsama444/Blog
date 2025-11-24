using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
    public class ChatUser
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedOnUtc { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public virtual Chat Chat { get; set; } = default!;
        public virtual User User { get; set; } = default!;
        // public static ChatUser Create(Guid ChatId, Guid UserId, bool IsAdmin, bool IsOwner)
        // {
        //     return new ChatUser
        //     {

        //     };
        // }
    }
}