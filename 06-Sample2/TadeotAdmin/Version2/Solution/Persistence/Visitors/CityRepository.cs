using AuthenticationBase.Persistence.Repositories;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

namespace Persistence.Visitors;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public ApplicationDbContext? DbContext { get; }

    public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}