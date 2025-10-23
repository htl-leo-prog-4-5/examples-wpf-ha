namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class BacklogItemMapping
{
    public static void Map(this EntityTypeBuilder<BacklogItem> entity)
    {
        entity.ToTable("BacklogItem");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.Property(d => d.Description).HasMaxLength(2000);


        entity.HasMany(e => e.Comments);
        entity.HasMany(e => e.TeamMembers).WithMany(c => c.BacklogItems);
        entity.HasOne(e => e.Effort);
    }
}