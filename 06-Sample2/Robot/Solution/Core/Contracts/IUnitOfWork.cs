namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICarRepository         Car         { get; }
    public ICompetitionRepository Competition { get; }
    public IRaceRepository        Race        { get; }
    public IDriverRepository      Driver      { get; }
    public IMoveRepository        Move        { get; }
}