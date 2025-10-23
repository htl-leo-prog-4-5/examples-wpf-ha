namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class SampleInfoCsv
{
    public required string Key   { get; set; }
    public required string Value { get; set; }
}