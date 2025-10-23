using Base.Core.Entities;

namespace Core.Entities;

public class Comment : EntityObject
{
    public required string Description { get; set; }

    public int SeqNo { get; set; }

    public int          BacklogItemId { get; set; }
    public BacklogItem? BacklogItem   { get; set; }
}