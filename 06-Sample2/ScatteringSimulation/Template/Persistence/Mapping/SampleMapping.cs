namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class SampleMapping
{
    public static void Map(this EntityTypeBuilder<Sample> entity)
    {
        entity.ToTable("Sample");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => new { Id = d.SimulationId, d.SeqNo }).IsUnique();
    }
}