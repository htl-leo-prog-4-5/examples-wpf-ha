using Base.Core.Entities;

namespace Core.Entities;

public class BacklogItem : EntityObject
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public DateOnly CreationDate { get; set; }

    public int? Priority { get; set; }

    public int? EffortId { get; set; }

    public Effort? Effort { get; set; }

    public IList<TeamMember>? TeamMembers { get; set; }

    public IList<Comment>? Comments { get; set; }
}