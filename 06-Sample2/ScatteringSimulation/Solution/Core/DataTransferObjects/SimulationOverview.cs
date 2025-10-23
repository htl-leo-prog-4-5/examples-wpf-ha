namespace Core.DataTransferObjects;

public record SimulationOverview(
    int      Id,
    string   Name,
    DateOnly CreationDate,
    int?     CategoryId,
    string   Category,
    string   Origin,
    int      SampleCount
);