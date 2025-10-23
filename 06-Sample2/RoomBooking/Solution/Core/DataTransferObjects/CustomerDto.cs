namespace Core.DataTransferObjects;

public record CustomerDto(int Id, string? FirstName, string LastName, int CountOpenBookings);