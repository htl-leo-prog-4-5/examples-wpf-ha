namespace Core.Entities;

using System.Collections.Generic;

using Base.Core.Entities;

public class Driver : EntityObject
{
    public string Name { get; set; } = string.Empty;

    public IList<Race>? Races { get; set; }
}