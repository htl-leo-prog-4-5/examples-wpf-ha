namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class VoteMapping
{
    public static void Map(this EntityTypeBuilder<Vote> entity)
    {
        entity.ToTable("Vote");
        entity.HasKey(d => d.Id);

        entity.HasOne(e => e.Script);
        entity.HasOne(e => e.Competition);
    }
}