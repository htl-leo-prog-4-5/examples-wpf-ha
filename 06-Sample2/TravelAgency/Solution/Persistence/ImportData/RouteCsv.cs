namespace Persistence.ImportData;

public class RouteCsv
{
    public required string Name          { get; set; }
    public required string TransportType { get; set; }
    public required string TransportInfo { get; set; }
    public required string Description   { get; set; }
    public required string StartLocation { get; set; }
    public required string EndLocation   { get; set; }
}