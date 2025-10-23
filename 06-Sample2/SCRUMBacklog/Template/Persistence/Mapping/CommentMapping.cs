namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CommentMapping
{
    public static void Map(this EntityTypeBuilder<Comment> entity)
    {
        entity.ToTable("Comment");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => new { Id = d.BacklogItemId, d.SeqNo}).IsUnique();
        entity.Property(d => d.Description).AsRequiredText(256);
    }
}