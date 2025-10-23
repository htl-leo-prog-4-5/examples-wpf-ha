using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

using Base.Core.Entities;

public class Office : EntityObject
{
    public required string No      { get; set; }
    public required string Name    { get; set; }
    public required string Address { get; set; }

    public string? City     { get; set; }
    public string? PLZ      { get; set; }
    public string? Street   { get; set; }
    public string? StreetNo { get; set; }

    public int?   StateId { get; set; }
    public State? State   { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}