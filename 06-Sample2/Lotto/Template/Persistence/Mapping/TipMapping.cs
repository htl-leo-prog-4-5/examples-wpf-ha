namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class TipMapping
{
    public static void Map(this EntityTypeBuilder<Tip> entity)
    {
        entity.ToTable("Tip");
        entity.HasKey(l => l.Id);

        entity.HasOne(t => t.Ticket).WithMany(t => t.Tips).HasForeignKey(t => t.TicketId);
    }
}