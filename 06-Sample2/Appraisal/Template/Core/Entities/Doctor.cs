using Base.Core.Entities;

namespace Core.Entities;

public class Doctor : EntityObject
{
    public required string Name { get; set; }
}