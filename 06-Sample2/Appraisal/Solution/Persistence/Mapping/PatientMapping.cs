namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class PatientMapping
{
    public static void Map(this EntityTypeBuilder<Patient> entity)
    {
        entity.ToTable("Patient");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.SVNumber).IsUnique();
        entity.Property(d => d.SVNumber).HasMaxLength(10);

        entity.Property(d => d.FirstName).HasMaxLength(256);
        entity.Property(d => d.LastName).HasMaxLength(256);
    }
}