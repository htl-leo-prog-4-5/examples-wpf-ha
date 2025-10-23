namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CompanionMapping
{
    public static void Map(this EntityTypeBuilder<Companion> entity)
    {
        entity.ToTable("Companion");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(256);

        entity.Property(d => d.Comment).HasMaxLength(2000);

    }
}