namespace Core.Entities;

using Base.Core.Entities;

public class Companion : EntityObject
{
    public required string Name { get; set; }

    public string? Comment { get; set; }

    public IList<Hike>? Hikes { get; set; }
}