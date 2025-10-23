namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class HikeMapping
{
    public static void Map(this EntityTypeBuilder<Hike> entity)
    {
        entity.ToTable("Hike");
        entity.HasKey(l => l.Id);

        entity.Property(d => d.Trail).AsRequiredText(256);

        entity.Property(d => d.Location).AsRequiredText(1024);

        entity.Property(d => d.Duration).AsDecimal(9, 2);
        entity.Property(d => d.Distance).AsDecimal(9, 2);

        entity.HasMany(e => e.Highlights);
        entity.HasMany(e => e.Companions).WithMany(c => c.Hikes);
        entity.HasOne(e => e.Difficulty);
    }
}