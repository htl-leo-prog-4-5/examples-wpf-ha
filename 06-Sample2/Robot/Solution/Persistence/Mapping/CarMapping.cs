namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CarMapping
{
    public static void Map(this EntityTypeBuilder<Car> entity)
    {
        entity.ToTable("Car");
        entity.HasKey(l => l.Id);

        entity.HasIndex(c => c.Name).IsUnique();
        entity.Property(c => c.Name).HasMaxLength(256);
        entity.Property(c => c.Comment).HasMaxLength(256);
    }
}