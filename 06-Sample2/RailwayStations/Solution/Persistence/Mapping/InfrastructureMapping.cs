namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class InfrastructureMapping
{
    public static void Map(this EntityTypeBuilder<Infrastructure> entity)
    {
        entity.ToTable("Infrastructure");
        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.HasIndex(d => d.Code).IsUnique();
        entity.Property(d => d.Code).AsRequiredText(32);

        entity.Property(d => d.City!).AsText(128);
        entity.Property(d => d.PLZ!).AsText(16);
        entity.Property(d => d.Street!).AsText(128);
        entity.Property(d => d.StreetNo!).AsText(16);
    }
}