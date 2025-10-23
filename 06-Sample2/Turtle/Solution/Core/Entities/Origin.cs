namespace Core.Entities;

using Base.Core.Entities;

public class Origin : EntityObject
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public IList<Script>? Script { get; set; }
}