namespace Persistence.ImportData;

using Base.Tools.CsvImport;

[CsvImportFormat(Culture = "de")]
internal class BacklogItemCsv
{
    public required string  Name        { get; set; }
    public          string? Description { get; set; }

    [CsvImportFormat(Format = "dd.MM.yyyy")]
    public DateOnly Date { get; set; }

    public int? Priority { get; set; }


    public required string Effort { get; set; }

    public required string Comments { get; set; }

    public required string TeamMembers { get; set; }
}