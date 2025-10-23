namespace Persistence.Mapping;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class TicketMapping
{
    public static void Map(this EntityTypeBuilder<Ticket> entity)
    {
        entity.ToTable("Ticket");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => d.TicketNo).IsUnique();
        entity.Property(d => d.TicketNo).IsUnicode(false).HasMaxLength(64);

        entity.HasOne(t => t.Office).WithMany(o => o.Tickets).HasForeignKey(t => t.OfficeId);
        entity.HasOne(t => t.Game).WithMany(g => g.Tickets).HasForeignKey(t => t.GameId);
    }
}