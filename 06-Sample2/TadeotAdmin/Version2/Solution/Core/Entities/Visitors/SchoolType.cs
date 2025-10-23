using AuthenticationBase.Entities;

using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Visitors;

public class SchoolType : EntityObject
{
    [MaxLength(300)]
    public string Type { get; set; } = string.Empty;

    public int Rank { get; set; }
}