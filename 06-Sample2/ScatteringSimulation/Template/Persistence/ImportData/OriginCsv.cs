namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class OriginCsv
{
    public required string Code { get; set; }
    public required string Name { get; set; }
}