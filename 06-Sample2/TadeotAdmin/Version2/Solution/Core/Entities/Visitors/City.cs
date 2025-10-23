using AuthenticationBase.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Visitors;

public class City : EntityObject
{
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(5)]
    public string ZipCode { get; set; } = string.Empty;

    public int Number { get; set; }

    [ForeignKey(nameof(DistrictId))]
    public District? District { get; set; }

    public int DistrictId { get; set; }

    public IList<Visitor> Visitors { get; set; } = new List<Visitor>();
}