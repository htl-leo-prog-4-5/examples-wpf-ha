using Base.Core.Entities;

namespace Core.Entities;

public class Ticket : EntityObject
{
    public required string TicketNo { get; set; }

    public required DateTime Created { get; set; }

    public int     OfficeId { get; set; }
    public Office? Office   { get; set; }

    public int   GameId { get; set; }
    public Game? Game   { get; set; }

    public ICollection<Tip>? Tips { get; set; }
}