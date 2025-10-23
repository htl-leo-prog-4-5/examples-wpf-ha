namespace Core.Entities;

public class Pupil : EntityObject
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName  { get; set; } = String.Empty;

    public string FullName => $"{LastName} {FirstName}";
}