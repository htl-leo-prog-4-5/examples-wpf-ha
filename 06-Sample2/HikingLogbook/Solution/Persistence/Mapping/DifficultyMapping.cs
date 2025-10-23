namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class DifficultyMapping
{
    public static void Map(this EntityTypeBuilder<Difficulty> entity)
    {
        entity.ToTable("Difficulty");
        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Description).IsUnique();
        entity.Property(d => d.Description).AsRequiredText(256);

        entity.Property(d => d.Comment).HasMaxLength(2000);
    }
}