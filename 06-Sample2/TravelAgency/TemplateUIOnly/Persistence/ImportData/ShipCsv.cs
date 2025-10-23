namespace Persistence.ImportData;

public class ShipCsv
{
    public required string  Name              { get; set; }
    public required string  Owner             { get; set; }
    public          int?     PassengerCapacity { get; set; }
    public          int?     CargoCapacity     { get; set; }
    public          decimal? MaxSpeed          { get; set; }
}