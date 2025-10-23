namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class TripMapping
{
    public static void Map(this EntityTypeBuilder<Trip> entity)
    {
        entity.ToTable("Trip");
        entity.HasKey(t => t.Id);

        entity.HasOne(t => t.Route).WithMany().HasForeignKey(t => t.RouteId);
    }
}