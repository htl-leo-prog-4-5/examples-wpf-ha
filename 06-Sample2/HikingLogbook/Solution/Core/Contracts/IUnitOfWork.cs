namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IHighlightRepository  HighlightRepository  { get; }
    public IDifficultyRepository DifficultyRepository { get; }
    public IHikeRepository       HikeRepository       { get; }
    public ICompanionRepository  CompanionRepository  { get; }
}