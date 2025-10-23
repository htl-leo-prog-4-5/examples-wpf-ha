namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class HighlightMapping
{
    public static void Map(this EntityTypeBuilder<Highlight> entity)
    {
        entity.ToTable("Highlight");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => new { d.HikeId, d.Description }).IsUnique();
        entity.Property(d => d.Description).AsRequiredText(256);

        entity.Property(d => d.Comment).HasMaxLength(2000);
    }
}