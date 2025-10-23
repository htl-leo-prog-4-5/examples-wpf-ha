namespace Core.DataTransferObjects;

public class SchoolLockerDto
{
    public int Number        { get; set; }
    public int CountBookings { get; set; }

    public bool IsTodayFree { get; set; }
}