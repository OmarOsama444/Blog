using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigs;

public class KgmEntityConfig : IEntityTypeConfiguration<KgmEntity>
{
    public void Configure(EntityTypeBuilder<KgmEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Dolphone)
            .HasMaxLength(100)
            .IsRequired();
        builder
            .HasIndex(x => x.Dolphone)
            .IsUnique();
    }

}
