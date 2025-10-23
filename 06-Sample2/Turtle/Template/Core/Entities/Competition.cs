using Base.Core.Entities;

namespace Core.Entities;

public class Competition : EntityObject
{
    public required string Description { get; set; }

    public bool Active { get; set; }

    public IList<Script>? Scripts { get; set; }
}