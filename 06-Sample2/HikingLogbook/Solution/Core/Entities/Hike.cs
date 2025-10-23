using Base.Core.Entities;

namespace Core.Entities;

public class Hike : EntityObject
{
    public required string Trail { get; set; }

    public DateOnly Date { get; set; }

    public required string Location { get; set; }

    public decimal Duration { get; set; }

    public decimal Distance { get; set; }

    public int         DifficultyId { get; set; }
    public Difficulty? Difficulty   { get; set; }

    public IList<Companion>? Companions { get; set; }
    public IList<Highlight>? Highlights { get; set; }
}