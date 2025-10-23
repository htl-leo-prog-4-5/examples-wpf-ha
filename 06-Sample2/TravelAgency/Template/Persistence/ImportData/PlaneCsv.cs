namespace Persistence.ImportData;

public class PlaneCsv
{
    public required string Model             { get; set; }
    public required string Manufacturer      { get; set; }
    public          int    PassengerCapacity { get; set; }
    public          int    CargoCapacity     { get; set; }
    public          int    MaxSpeed          { get; set; }
}