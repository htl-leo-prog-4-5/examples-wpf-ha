using Base.Core.Entities;

namespace Core.Entities;

public class Simulation : EntityObject
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public DateOnly CreationDate { get; set; }

    public int? CategoryId { get; set; }

    public Category? Category { get; set; }

    public int? OriginId { get; set; }

    public Origin? Origin { get; set; }


    public IList<Sample>? Samples { get; set; }
}