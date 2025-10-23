namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class RaceMapping
{
    public static void Map(this EntityTypeBuilder<Race> entity)
    {
        entity.ToTable("Race");
        entity.HasKey(r => r.Id);
        entity.HasOne(r => r.Driver).WithMany(d => d.Races).HasForeignKey(r => r.DriverId);
        entity.HasOne(r => r.Competition).WithMany(c => c.Races).HasForeignKey(r => r.CompetitionId);
        //TODO
    }
}