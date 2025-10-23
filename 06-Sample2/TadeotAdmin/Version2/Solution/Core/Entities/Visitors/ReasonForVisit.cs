using AuthenticationBase.Entities;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Visitors;

[Index(nameof(Rank), IsUnique = true)]
public class ReasonForVisit : EntityObject
{
    [MaxLength(300)]
    public string Reason { get; set; } = string.Empty;

    public int Rank { get; set; }
}