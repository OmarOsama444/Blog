using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs
{
    public class ChatRequesConfig : IEntityTypeConfiguration<ChatRequest>
    {
        public void Configure(EntityTypeBuilder<ChatRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ToUserId);
            builder.HasIndex(x => x.FromUserId);
        }
    }
}