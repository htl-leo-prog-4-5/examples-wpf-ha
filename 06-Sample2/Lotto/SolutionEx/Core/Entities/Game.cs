using Base.Core.Entities;

namespace Core.Entities;

public class Game : EntityObject
{
    public required DateOnly DateFrom         { get; set; }
    public required DateOnly DateTo           { get; set; }
    public required DateOnly ExpectedDrawDate { get; set; }

    public int MaxNo { get; set; }

    public DateTime? DrawDate { get; set; }

    public byte? No1 { get; set; }
    public byte? No2 { get; set; }
    public byte? No3 { get; set; }
    public byte? No4 { get; set; }
    public byte? No5 { get; set; }
    public byte? No6 { get; set; }
    public byte? NoX { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }

    public int? Count3   { get; set; }
    public int? Count4   { get; set; }
    public int? Count5   { get; set; }
    public int? Count6   { get; set; }
    public int? Count5ZZ { get; set; }
}