using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public IDifficultyRepository DifficultyRepository { get; }
    public IHighlightRepository  HighlightRepository  { get; }
    public IHikeRepository       HikeRepository       { get; }
    public ICompanionRepository  CompanionRepository  { get; }

    public UnitOfWork(ApplicationDbContext context,
        IDifficultyRepository              difficultyRepository,
        IHighlightRepository               highlightRepository,
        IHikeRepository                    hikeRepository,
        ICompanionRepository               companionRepository
    ) : base(context)
    {
        DifficultyRepository = difficultyRepository;
        HighlightRepository  = highlightRepository;
        HikeRepository       = hikeRepository;
        CompanionRepository  = companionRepository;
    }
}