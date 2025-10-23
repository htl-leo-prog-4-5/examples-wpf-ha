using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

[Index(nameof(Number), IsUnique = true)]
public class Locker : EntityObject
{
    [Display(Name = "Spindnummer")]
    public int Number { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}