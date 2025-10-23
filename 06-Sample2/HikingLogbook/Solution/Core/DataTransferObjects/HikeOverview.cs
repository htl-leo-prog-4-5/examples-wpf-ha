namespace Core.DataTransferObjects;

public record HikeOverview(
    int      Id,
    string   Trail,
    DateOnly Date,
    string   Location,
    decimal  Duration,
    decimal  Distance,
    string   Difficulty,
    string   Companions,
    string   Highlights
);