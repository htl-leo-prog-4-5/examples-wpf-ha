namespace Core.DataTransferObjects;

public record ScriptOverview(
    int      Id,
    string   Name,
    DateOnly CreationDate,
    int?     OriginId,
    string   Origin,
    int      MoveCount,
    string   Competitions
);