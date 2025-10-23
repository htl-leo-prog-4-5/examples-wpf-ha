namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ScriptMapping
{
    public static void Map(this EntityTypeBuilder<Script> entity)
    {
        entity.ToTable("Script");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.Property(d => d.Description).HasMaxLength(2000);


        entity.HasMany(s => s.Moves);
        entity.HasOne(s => s.Origin);

        entity.HasMany(s => s.Competitions).WithMany(c => c.Scripts);
    }
}