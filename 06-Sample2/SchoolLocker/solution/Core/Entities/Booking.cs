using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Booking : EntityObject
{
    public int     LockerId { get; set; }
    public Locker? Locker   { get; set; }

    [Display(Name = "Schüler:in")]
    public int PupilId { get; set; }

    public Pupil? Pupil { get; set; }

    [Display(Name = "Start")]
    //[FromToValidation]
    public DateTime From { get; set; }

    [Display(Name = "Ende")]
    //[FromToValidation]
    public DateTime? To { get; set; }
}