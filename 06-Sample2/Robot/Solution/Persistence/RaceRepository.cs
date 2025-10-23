namespace Persistence;

using System.Linq;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

public class RaceRepository : GenericRepository<Race>, IRaceRepository
{
    public RaceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetRaceCount(string driverName, string competitionName)
    {
        return await DbSet
            .Include(nameof(Race.Competition))
            .Include(nameof(Race.Driver))
            .Where(r => r.Driver!.Name == driverName && r.Competition!.Name == competitionName)
            .CountAsync();
    }
}