namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class DriverMapping
{
    public static void Map(this EntityTypeBuilder<Driver> entity)
    {
        entity.ToTable("Driver");
        entity.HasKey(d => d.Id);
        //TODO
    }
}