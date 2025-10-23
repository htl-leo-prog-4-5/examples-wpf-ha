namespace Wpf.ViewModels.Models;

public class Comment
{
    public int Id { get; set; }

    public int No { get; set; }

    public int SeqNo { get; set; }

    public required string Description { get; set; }

}