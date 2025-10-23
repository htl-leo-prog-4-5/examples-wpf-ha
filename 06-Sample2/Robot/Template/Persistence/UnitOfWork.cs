namespace Persistence;

using Core.Contracts;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ApplicationDbContext? ApplicationDbContext => BaseApplicationDbContext as ApplicationDbContext;

    public UnitOfWork(
        ApplicationDbContext   applicationDbContext,
        ICompetitionRepository competition,
        IDriverRepository      driver,
        IRaceRepository        race,
        IMoveRepository        move,
        ICarRepository         car
    ) : base(applicationDbContext)
    {
        Driver      = driver;
        Race        = race;
        Move        = move;
        Competition = competition;
        Car         = car;
    }

    public IMoveRepository        Move        { get; }
    public IRaceRepository        Race        { get; }
    public IDriverRepository      Driver      { get; }
    public ICompetitionRepository Competition { get; }
    public ICarRepository         Car         { get; }
}