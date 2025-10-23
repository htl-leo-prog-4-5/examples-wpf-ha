namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class ExaminationCsv
{
    public required string Key   { get; set; }
    public required string Value { get; set; }
}