namespace Core.DataTransferObjects;

public record VoteResult(
    int    ScriptId,
    string Name,
    int    Count1,
    int    Count2,
    int    Count3,
    int    Count4,
    int    Count5
);