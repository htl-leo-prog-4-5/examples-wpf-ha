namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class RouteStepMapping
{
    public static void Map(this EntityTypeBuilder<RouteStep> entity)
    {
        entity.ToTable("RouteStep");
        entity.HasKey(l => l.Id);

        entity.Property(d => d.Description).HasMaxLength(128);

        entity.HasOne(s => s.Hotel).WithMany().HasForeignKey(s => s.HotelId);
        entity.HasOne(s => s.Plane).WithMany().HasForeignKey(s => s.PlaneId);
        entity.HasOne(s => s.Ship).WithMany().HasForeignKey(s => s.ShipId);

        entity.HasOne(s => s.Route).WithMany(r => r.Steps).HasForeignKey(s => s.RouteId);
    }
}