namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CompetitionMapping
{
    public static void Map(this EntityTypeBuilder<Competition> entity)
    {
        entity.ToTable("Competition");
        entity.HasKey(l => l.Id);
        //TODO
    }
}