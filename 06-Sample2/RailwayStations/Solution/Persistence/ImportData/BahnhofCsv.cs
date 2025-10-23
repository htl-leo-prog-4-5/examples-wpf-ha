namespace Persistence.ImportData;

internal class BahnhofCsv
{
    public required string  Name              { get; set; }
    public          string  Abk               { get; set; } = string.Empty;
    public          string  Art               { get; set; } = string.Empty;
    public          string  Standort­gemeinde { get; set; } = string.Empty;
    public          string  BL                { get; set; } = string.Empty;
    public          string  IB                { get; set; } = string.Empty;
    public          string  EVU               { get; set; } = string.Empty;
    public          string  Strecke           { get; set; } = string.Empty;
    public          string? FV                { get; set; } = string.Empty;
    public          string? RV                { get; set; } = string.Empty;
    public          string? SB                { get; set; } = string.Empty;
    public          string  Bemer­kungen      { get; set; } = string.Empty;
}