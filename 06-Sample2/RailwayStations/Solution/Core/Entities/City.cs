using Base.Core.Entities;

namespace Core.Entities;

public class City : EntityObject
{
    public required string Name { get; set; }
}