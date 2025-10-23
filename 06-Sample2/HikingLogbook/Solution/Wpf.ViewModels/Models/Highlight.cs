namespace Wpf.ViewModels.Models;

public class Highlight
{
    public int Id { get; set; }

    public int No { get; set; }

    public required string Description { get; set; }

    public string? Comment { get; set; }
}