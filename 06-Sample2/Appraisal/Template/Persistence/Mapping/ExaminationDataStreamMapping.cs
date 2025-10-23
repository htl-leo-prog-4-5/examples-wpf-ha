namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ExaminationDataStreamMapping
{
    public static void Map(this EntityTypeBuilder<ExaminationDataStream> entity)
    {
        entity.ToTable("ExaminationDataStream");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => new { Id = d.Id, d.Name }).IsUnique();

        entity.Property(e => e.Values).HasMaxLength(-1);
    }
}