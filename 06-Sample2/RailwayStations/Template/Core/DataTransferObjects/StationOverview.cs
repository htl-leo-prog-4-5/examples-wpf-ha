namespace Core.DataTransferObjects;

public record StationOverview(int Id, string StationName, string? StationCode, string City, string Lines);