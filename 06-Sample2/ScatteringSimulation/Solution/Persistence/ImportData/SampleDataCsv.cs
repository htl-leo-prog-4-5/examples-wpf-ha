namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class CategoryCsv
{
    public required string Name { get; set; }
}