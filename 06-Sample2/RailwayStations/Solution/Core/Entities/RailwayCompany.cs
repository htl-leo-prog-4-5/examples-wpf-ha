using Base.Core.Entities;

namespace Core.Entities;

public class RailwayCompany : EntityObject
{
    public required string Code { get; set; }
    public required string Name { get; set; }

    public string? City     { get; set; }
    public string? PLZ      { get; set; }
    public string? Street   { get; set; }
    public string? StreetNo { get; set; }
}