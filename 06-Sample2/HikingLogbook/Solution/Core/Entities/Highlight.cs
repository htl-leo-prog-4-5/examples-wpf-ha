using Base.Core.Entities;

namespace Core.Entities;

public class Highlight : EntityObject
{
    public required string Description { get; set; }

    public string? Comment { get; set; }

    public int   HikeId { get; set; }
    public Hike? Hike   { get; set; }
}