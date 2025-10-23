namespace Core.DataTransferObjects;

public record CompetitionVoteResult(
    int               Id,
    string            Name,
    IList<VoteResult> Votes
);