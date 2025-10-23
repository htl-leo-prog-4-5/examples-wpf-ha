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
    }
}