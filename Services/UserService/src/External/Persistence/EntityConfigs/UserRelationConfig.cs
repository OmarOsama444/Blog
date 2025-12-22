using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs
{
    public class UserRelationConfig : IEntityTypeConfiguration<UserRelation>
    {
        public void Configure(EntityTypeBuilder<UserRelation> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasIndex(x => new { x.FromId, x.ToId, x.Relation })
                .IsUnique();
            builder
                .HasIndex(x => x.Relation);
            builder
                .HasIndex(x => x.Status);
            builder
                .HasOne(x => x.ToUser)
                .WithMany(x => x.ToUserRelations)
                .HasForeignKey(x => x.ToId);
            builder
                .HasOne(x => x.FromUser)
                .WithMany(x => x.FromUserRelations)
                .HasForeignKey(x => x.FromId);

        }
    }
}