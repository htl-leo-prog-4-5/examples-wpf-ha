namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ExaminationMapping
{
    public static void Map(this EntityTypeBuilder<Examination> entity)
    {
        entity.ToTable("Examination");
        entity.HasKey(l => l.Id);

        entity.Property(d => d.MedicalFindings).HasMaxLength(-1);

        entity.HasMany(s => s.DataStreams);
        entity.HasOne(s => s.Patient);
        entity.HasOne(s => s.Doctor);
    }
}