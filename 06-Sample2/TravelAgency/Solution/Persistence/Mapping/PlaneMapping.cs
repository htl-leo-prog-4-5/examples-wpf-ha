namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class PlaneMapping
{
    public static void Map(this EntityTypeBuilder<Plane> entity)
    {
        entity.ToTable("Plane");
        entity.HasKey(d => d.Id);
        entity.HasIndex(d => d.Model).IsUnique();
        entity.Property(d => d.Model).AsRequiredText(256);
        entity.Property(d => d.Manufacturer).AsRequiredText(512);
    }
}