using System;

using Base.Tools.CsvImport;

namespace ImportConsoleApp.ImportData;

public class CsvContent
{
    public required string Competition { get; set; }
    public required string Driver      { get; set; }
    public required string Car         { get; set; }
    [CsvImportFormat(Format = "dd.MM.yyyy HH:mm")]
    public DateTime RaceStartTime { get; set; }
    [CsvImportFormat(Format = "mm:ss")]
    public TimeOnly RaceTime { get;     set; }
    public required string Moves { get; set; }
}