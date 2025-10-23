namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CityMapping
{
    public static void Map(this EntityTypeBuilder<City> entity)
    {
        entity.ToTable("City");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).HasMaxLength(256);
    }
}