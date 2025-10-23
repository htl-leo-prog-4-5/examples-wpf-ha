namespace Core.Entities;

using Base.Core.Entities;

public class Route : EntityObject
{
    public required string Name { get; set; }

    public IList<RouteStep>? Steps { get; set; }
}