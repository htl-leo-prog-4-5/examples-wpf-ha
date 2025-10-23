namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class LineMapping
{
    public static void Map(this EntityTypeBuilder<Line> entity)
    {
        entity.ToTable("Line");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(64);

        entity.Property(e => e.Length).AsDecimal(9, 3);
    }
}