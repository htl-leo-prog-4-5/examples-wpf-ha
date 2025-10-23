using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ITripRepository      TripRepository      { get; }
    public UnitOfWork(ApplicationDbContext context,
        ITripRepository                    tripRepository

    ) : base(context)
    {
        TripRepository      = tripRepository;
    }
}