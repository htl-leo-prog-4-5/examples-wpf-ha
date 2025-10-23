namespace Persistence.ImportData;

using Base.Tools.CsvImport;

public class GamesCsv
{
    [CsvImportFormat(Format = "dd.MM.yyyy")]
    public DateOnly Date { get; set; }
    public byte No1 { get; set; }
    public byte No2 { get; set; }
    public byte No3 { get; set; }
    public byte No4 { get; set; }
    public byte No5 { get; set; }
    public byte No6 { get; set; }
    public byte ZZ { get; set; }
}