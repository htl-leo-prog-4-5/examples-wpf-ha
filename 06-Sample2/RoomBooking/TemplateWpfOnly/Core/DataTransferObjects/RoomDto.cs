using Core.Entities;

namespace Core.DataTransferObjects;

public class RoomDto
{
    public int    RoomId     { get; set; }
    public string RoomNumber { get; set; } = string.Empty;

    public RoomType RoomType { get; set; }

    public bool CurrentlyAvailable
    {
        get { return CurrentBooking == null; }
    }

    public Booking? CurrentBooking { get; set; }

    public override string ToString()
    {
        return $"{RoomNumber} {RoomType}";
    }
}