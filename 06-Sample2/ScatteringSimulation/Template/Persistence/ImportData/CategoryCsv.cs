namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class SampleDataCsv
{
    public required double X     { get; set; }
    public required double Y     { get; set; }
    public required double Value { get; set; }
}