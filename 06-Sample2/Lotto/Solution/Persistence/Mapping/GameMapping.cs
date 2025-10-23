namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class GameMapping
{
    public static void Map(this EntityTypeBuilder<Game> entity)
    {
        entity.ToTable("Game");
        entity.HasKey(l => l.Id);
    }
}