namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IMoveRepository        MoveRepository        { get; }
    public ICompetitionRepository CompetitionRepository { get; }
    public IScriptRepository      ScriptRepository      { get; }
    public IOriginRepository      OriginRepository      { get; }
    public IVoteRepository        VoteRepository        { get; }
}