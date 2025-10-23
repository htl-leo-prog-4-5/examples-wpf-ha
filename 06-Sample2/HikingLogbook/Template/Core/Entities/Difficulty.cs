namespace Core.Entities;

using Base.Core.Entities;

public class Difficulty : EntityObject
{
    public required string Description { get; set; }

    public string? Comment { get; set; }
}