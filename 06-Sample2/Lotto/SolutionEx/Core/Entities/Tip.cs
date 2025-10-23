using Base.Core.Entities;

namespace Core.Entities;

public class Tip : EntityObject
{
    public Ticket? Ticket   { get; set; }
    public int     TicketId { get; set; }

    public required byte No1 { get; set; }
    public required byte No2 { get; set; }
    public required byte No3 { get; set; }
    public required byte No4 { get; set; }
    public required byte No5 { get; set; }
    public required byte No6 { get; set; }
}