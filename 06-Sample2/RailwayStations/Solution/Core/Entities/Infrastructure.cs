namespace Core.Entities;

using Base.Core.Entities;

public class Infrastructure : EntityObject
{
    public required string Code { get; set; }
    public required string Name    { get; set; }

    public string? City     { get; set; }
    public string? PLZ      { get; set; }
    public string? Street   { get; set; }
    public string? StreetNo { get; set; }
}