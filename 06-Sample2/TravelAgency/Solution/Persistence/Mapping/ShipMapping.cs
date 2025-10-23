namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ShipMapping
{
    public static void Map(this EntityTypeBuilder<Ship> entity)
    {
        entity.ToTable("Ship");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);
        entity.Property(d => d.Owner).AsText(512);

        entity.Property(d => d.MaxSpeed).AsDecimal(5, 2);
    }
}