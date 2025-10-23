namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class StateMapping
{
    public static void Map(this EntityTypeBuilder<State> entity)
    {
        entity.ToTable("State");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(64);
    }
}