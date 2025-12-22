using Domain.Abstractions;
using Domain.Events;

namespace Domain.Entities
{
    public class Post : Entity
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public virtual User User { get; set; } = default!;
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public List<float> Embeddings { get; set; } = [];
        public float Rating { get; set; } = 0.0f;
        public uint TotalUsersRated { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = [];

        public static Post Create(Guid UserId, string Slug, string Title, string Content, ICollection<string> Tags,
            ICollection<float> Embeddings)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Embeddings = [.. Embeddings],
                UserId = UserId,
                Slug = Slug,
                Title = Title,
                Content = Content,
                Tags = [.. Tags],
                CreatedOnUtc = DateTime.UtcNow
            };
            post.RaiseDomainEvent(new PostCreatedDomainEvent(post.Id));
            return post;
        }

        public void DeletePost()
        {
            RaiseDomainEvent(new PostDeletedDomainEvent(Id));
        }

        public void UpdateAverageRating(byte rating, bool isUpdate = false, byte? oldRating = null)
        {
            if (isUpdate && oldRating.HasValue)
            {
                Rating = ((Rating * TotalUsersRated) - oldRating.Value + rating) / TotalUsersRated;
            }
            else
            {
                Rating = ((Rating * TotalUsersRated) + rating) / (TotalUsersRated + 1);
                TotalUsersRated += 1;
            }
        }
    }
}