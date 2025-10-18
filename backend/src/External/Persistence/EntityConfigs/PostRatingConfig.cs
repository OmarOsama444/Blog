using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs;

public class PostRatingConfig : IEntityTypeConfiguration<PostRating>
{
    public void Configure(EntityTypeBuilder<PostRating> builder)
    {
        builder.HasKey(pr => new { pr.PostId, pr.UserId });
    }
}