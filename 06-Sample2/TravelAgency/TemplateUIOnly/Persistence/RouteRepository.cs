using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class RouteRepository : GenericRepository<Route>, IRouteRepository
{
    public RouteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}