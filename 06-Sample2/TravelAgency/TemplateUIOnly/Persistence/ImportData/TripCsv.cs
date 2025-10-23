namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
public class TripCsv
{
    public required string   RouteName         { get; set; }

    [CsvImportFormat(Format = "yyyy.MM.dd")]
    public DateTime DepartureDateTime { get; set; }
    
    [CsvImportFormat(Format = "yyyy.MM.dd")]
    public DateTime ArrivalDateTime   { get; set; }
}