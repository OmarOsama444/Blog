using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId);
            builder.Property(x => x.Slug).IsRequired();
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.HasIndex(x => x.Title);
            builder.HasIndex(x => x.Content);
            builder.HasIndex(x => new { x.Title, x.Content })
                .HasMethod("GIN")
                .IsTsVectorExpressionIndex("english");
        }
    }
}