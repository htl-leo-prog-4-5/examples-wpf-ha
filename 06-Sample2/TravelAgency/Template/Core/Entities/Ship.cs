using Base.Core.Entities;

namespace Core.Entities;

public class Ship : EntityObject
{
    public required string  Name              { get; set; }
    public required string  Owner             { get; set; }
    public          int     PassengerCapacity { get; set; }
    public          int     CargoCapacity     { get; set; }
    public          decimal MaxSpeed          { get; set; }
}