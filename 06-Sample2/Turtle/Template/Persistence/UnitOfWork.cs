using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ICompetitionRepository CompetitionRepository { get; }
    public IMoveRepository        MoveRepository        { get; }
    public IScriptRepository      ScriptRepository      { get; }
    public IOriginRepository      OriginRepository      { get; }
    public IVoteRepository        VoteRepository        { get; }

    public UnitOfWork(ApplicationDbContext context,
        ICompetitionRepository             competitionRepository,
        IMoveRepository                    moveRepository,
        IScriptRepository                  scriptRepository,
        IOriginRepository                  originRepository,
        IVoteRepository                    voteRepository
    ) : base(context)
    {
        CompetitionRepository = competitionRepository;
        MoveRepository        = moveRepository;
        ScriptRepository      = scriptRepository;
        OriginRepository      = originRepository;
        VoteRepository        = voteRepository;
    }
}