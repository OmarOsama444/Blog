using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public virtual User User { get; set; } = default!;
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public DateTime CreatedOnUtc { get; set; }

    }
}