namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class HotelMapping
{
    public static void Map(this EntityTypeBuilder<Hotel> entity)
    {
        entity.ToTable("Hotel");
        entity.HasKey(d => d.Id);
        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);
        entity.Property(d => d.Location).AsRequiredText(512);

        entity.Property(d => d.PricePerNight).AsDecimal(12, 2);
        entity.Property(d => d.Rating).AsDecimal(4, 2);
    }
}