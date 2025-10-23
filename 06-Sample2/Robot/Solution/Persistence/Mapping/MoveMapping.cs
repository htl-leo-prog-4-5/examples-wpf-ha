namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class MoveMapping
{
    public static void Map(this EntityTypeBuilder<Move> entity)
    {
        entity.ToTable("Move");
        entity.HasKey(m => m.Id);
        entity.HasOne(m => m.Race).WithMany(r => r.Moves).HasForeignKey(m => m.RaceId);
    }
}