namespace Core.Entities;

using Base.Core.Entities;

public class Line : EntityObject
{
    public required string Name { get; set; }

    public decimal? Length { get; set; }

    public bool? IsElectric { get; set; }
}