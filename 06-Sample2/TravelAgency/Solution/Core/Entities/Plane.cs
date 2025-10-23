namespace Core.Entities;

using Base.Core.Entities;

public class Plane : EntityObject
{
    public required string Model             { get; set; }
    public required string Manufacturer      { get; set; }
    public          int    PassengerCapacity { get; set; }
    public          int    CargoCapacity     { get; set; }
    public          int    MaxSpeed          { get; set; }
}