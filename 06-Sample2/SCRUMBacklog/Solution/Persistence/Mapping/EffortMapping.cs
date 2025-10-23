namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EffortMapping
{
    public static void Map(this EntityTypeBuilder<Effort> entity)
    {
        entity.ToTable("Effort");
        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Description).IsUnique();
        entity.Property(d => d.Description).AsRequiredText(256);
    }
}