namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CompetitionMapping
{
    public static void Map(this EntityTypeBuilder<Competition> entity)
    {
        entity.ToTable("Competition");
        entity.HasKey(d => d.Id);

        entity.HasIndex(d => d.Description).IsUnique();
        entity.Property(d => d.Description).AsRequiredText(256);
    }
}