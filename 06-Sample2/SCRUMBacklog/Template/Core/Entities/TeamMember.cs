namespace Core.Entities;

using Base.Core.Entities;

public class TeamMember : EntityObject
{
    public required string Name { get; set; }

    public IList<BacklogItem>? BacklogItems { get; set; }
}