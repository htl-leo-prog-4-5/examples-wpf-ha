namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class SimulationMapping
{
    public static void Map(this EntityTypeBuilder<Simulation> entity)
    {
        entity.ToTable("Simulation");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.Property(d => d.Description).HasMaxLength(2000);


        entity.HasMany(e => e.Samples);
        entity.HasOne(e => e.Category);
        entity.HasOne(e => e.Origin);
    }
}