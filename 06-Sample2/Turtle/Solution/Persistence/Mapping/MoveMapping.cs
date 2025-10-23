namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class MoveMapping
{
    public static void Map(this EntityTypeBuilder<Move> entity)
    {
        entity.ToTable("Move");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => new { Id = d.ScriptId, d.SeqNo }).IsUnique();
    }
}