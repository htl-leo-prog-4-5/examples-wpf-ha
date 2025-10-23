using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class TripRepository : GenericRepository<Trip>, ITripRepository
{
    public TripRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}