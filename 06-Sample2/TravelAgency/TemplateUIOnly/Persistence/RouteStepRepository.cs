using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class RouteStepRepository : GenericRepository<RouteStep>, IRouteStepRepository
{
    public RouteStepRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}