using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs
{
    public class ChatMessageConfig : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired();
            builder.HasOne(x => x.Chat)
                .WithMany(x => x.ChatMessages)
                .HasForeignKey(x => x.ChatId);
            builder.HasOne(x => x.User)
                .WithMany(x => x.SentMessages)
                .HasForeignKey(x => x.SenderUserId);
        }
    }
}