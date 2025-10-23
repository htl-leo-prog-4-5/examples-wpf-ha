namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class DoctorMapping
{
    public static void Map(this EntityTypeBuilder<Doctor> entity)
    {
        entity.ToTable("Doctor");
        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);
    }
}