using AuthenticationBase.Entities;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Visitors;

[Index(nameof(Number), IsUnique = true)]
[Index(nameof(Name),   IsUnique = true)]
public class District : EntityObject
{
    [MaxLength(30)]
    public string Name { get; set; } = string.Empty;

    public int Number { get; set; }

    public IList<City> Cities { get; set; } = new List<City>();
}