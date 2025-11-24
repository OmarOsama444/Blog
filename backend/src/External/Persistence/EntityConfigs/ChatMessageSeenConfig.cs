using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs
{
    public class ChatMessageSeenConfig : IEntityTypeConfiguration<ChatMessageSeen>
    {
        public void Configure(EntityTypeBuilder<ChatMessageSeen> builder)
        {
            builder.HasKey(x => new { x.ChatMessageId, x.UserId });
            builder.HasOne(x => x.ChatMessage)
                .WithMany(x => x.SeenByUsers)
                .HasForeignKey(x => x.ChatMessageId);
            builder.HasOne(x => x.User)
                .WithMany(x => x.SeenMessages)
                .HasForeignKey(x => x.UserId);
        }
    }
}