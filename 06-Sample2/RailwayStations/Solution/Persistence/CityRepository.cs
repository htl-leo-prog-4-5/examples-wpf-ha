using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}