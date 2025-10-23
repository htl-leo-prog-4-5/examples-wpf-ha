namespace Core.Entities;

using System.Collections.Generic;

using Base.Core.Entities;

public class Car : EntityObject
{
    public string Name { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    //TODO Navigation properties
}