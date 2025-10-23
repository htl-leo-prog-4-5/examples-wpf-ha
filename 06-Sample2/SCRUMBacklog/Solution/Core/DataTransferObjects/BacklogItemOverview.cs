namespace Core.DataTransferObjects;

public record BacklogItemOverview(
    int      Id,
    string   Name,
    string?  Description,
    DateOnly CreationDate,
    int?     Priority,
    string   Effort,
    string   Comments,
    string   TeamMembers
);