using Base.Core.Entities;

namespace Core.Entities;

public class Script : EntityObject
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public DateOnly CreationDate { get; set; }

    public int? OriginId { get; set; }

    public Origin? Origin { get; set; }

    public IList<Move>? Moves { get; set; }

    public IList<Competition>? Competitions { get; set; }
}