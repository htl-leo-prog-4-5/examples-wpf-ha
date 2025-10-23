namespace Core.Entities;

using Base.Core.Entities;

public class Patient : EntityObject
{
    public required string SVNumber { get; set; }

    public string? FirstName { get; set; } = null;

    public string? LastName { get; set; } = null;

    public IList<Examination>? Examinations { get; set; }
}