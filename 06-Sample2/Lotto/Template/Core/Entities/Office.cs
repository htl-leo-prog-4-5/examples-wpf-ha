namespace Core.Entities;

using Base.Core.Entities;

public class Office : EntityObject
{
    public required string No      { get; set; }
    public required string Name    { get; set; }
    public required string Address { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}