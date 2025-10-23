namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class HikeCsv
{
    public required string Trail { get; set; }

    [CsvImportFormat(Format = "dd.MM.yyyy")]
    public DateOnly Date { get; set; }

    public required string Location { get; set; }

    public decimal Duration { get; set; }

    public decimal Distance { get; set; }

    public required string Difficulty { get; set; }

    public required string Companions { get; set; }

    public required string Highlights { get; set; }
}