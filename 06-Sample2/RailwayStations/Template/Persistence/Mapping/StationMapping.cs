namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class StationMapping
{
    public static void Map(this EntityTypeBuilder<Station> entity)
    {
        entity.ToTable("Station");
        entity.HasKey(l => l.Id);

        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.HasIndex(d => d.Code!).IsUnique();
        entity.Property(d => d.Code!).AsText(32);

        entity.Property(d => d.Type!).AsText(32);
        entity.Property(d => d.StateCode).AsRequiredText(32);

        entity.Property(d => d.Remark!).AsText(1024);

        entity.HasMany(e => e.Infrastructures).WithMany();
        entity.HasMany(e => e.Lines).WithMany();
        entity.HasMany(e => e.RailwayCompanies).WithMany();
    }
}