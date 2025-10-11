using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Post
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public virtual User User { get; set; } = default!;
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        [JsonIgnore]
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public DateTime CreatedOnUtc { get; set; }
        public static Post Create(Guid UserId, string Slug, string Title, string Content, ICollection<string> Tags)
        {
            return new Post
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                Slug = Slug,
                Title = Title,
                Content = Content,
                Tags = [.. Tags],
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}