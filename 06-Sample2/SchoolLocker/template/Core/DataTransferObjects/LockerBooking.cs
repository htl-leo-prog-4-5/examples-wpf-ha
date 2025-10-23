using Core.Entities;

namespace Core.DataTransferObjects;

public class LockerBooking
{
    public Locker Locker { get; set; } = new Locker();
    public int    Count  { get; set; }

    public DateTime From { get; set; }

    public DateTime? To { get; set; }
}