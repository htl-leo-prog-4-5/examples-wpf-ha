using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ICategoryRepository   CategoryRepository   { get; }
    public ISampleRepository     SampleRepository     { get; }
    public ISimulationRepository SimulationRepository { get; }
    public IOriginRepository     OriginRepository     { get; }

    public UnitOfWork(ApplicationDbContext context,
        ICategoryRepository                effortRepository,
        ISampleRepository                  commentRepository,
        ISimulationRepository              backlogItemRepository,
        IOriginRepository                  originRepository
    ) : base(context)
    {
        CategoryRepository   = effortRepository;
        SampleRepository     = commentRepository;
        SimulationRepository = backlogItemRepository;
        OriginRepository     = originRepository;
    }
}