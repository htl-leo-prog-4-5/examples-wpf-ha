using Base.Core.Entities;

namespace Core.Entities;

public class Game : EntityObject
{
    public required DateOnly DateFrom         { get; set; }
    public required DateOnly DateTo           { get; set; }

    public DateTime? DrawDate { get; set; }

    public byte? No1 { get; set; }
    public byte? No2 { get; set; }
    public byte? No3 { get; set; }
    public byte? No4 { get; set; }
    public byte? No5 { get; set; }
    public byte? No6 { get; set; }
    public byte? NoX { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}