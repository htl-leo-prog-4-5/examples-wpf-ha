namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class OfficeMapping
{
    public static void Map(this EntityTypeBuilder<Office> entity)
    {
        entity.ToTable("Office");
        entity.HasKey(d => d.Id);
        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);
        entity.Property(d => d.Address).AsRequiredText(512);

        entity.Property(d => d.City!).AsText(128);
        entity.Property(d => d.PLZ!).AsText(16);
        entity.Property(d => d.Street!).AsText(128);
        entity.Property(d => d.StreetNo!).AsText(16);

        entity.HasOne(s => s.State).WithMany().HasForeignKey(t => t.StateId);
    }
}