namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class OriginMapping
{
    public static void Map(this EntityTypeBuilder<Origin> entity)
    {
        entity.ToTable("Origin");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Code).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(16);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(256);
    }
}