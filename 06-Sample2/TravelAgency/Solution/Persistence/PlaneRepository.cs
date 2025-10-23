using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class PlaneRepository : GenericRepository<Plane>, IPlaneRepository
{
    public PlaneRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}