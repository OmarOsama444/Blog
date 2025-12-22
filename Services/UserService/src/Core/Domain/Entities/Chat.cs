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
        public bool Active { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; } = [];
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = [];
    }
}