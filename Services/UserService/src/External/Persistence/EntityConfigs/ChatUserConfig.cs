using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs;

public class ChatUserConfig : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.ChatId, x.UserId }).IsUnique();
        builder.HasOne(x => x.Chat)
            .WithMany(x => x.ChatUsers)
            .HasForeignKey(x => x.ChatId);
        builder.HasOne(x => x.User)
            .WithMany(x => x.ChatUsers)
            .HasForeignKey(x => x.UserId);
    }

}
