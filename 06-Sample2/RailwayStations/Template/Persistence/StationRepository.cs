using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

public class StationRepository : GenericRepository<Station>, IStationRepository
{
    public StationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<StationOverview>> GetStationOverviewAsync()
    {
        return await DbSet
            .Include(s => s.Lines)
            .Include(s => s.City)
            .OrderBy(s => s.Name)
            .Select(s => new StationOverview(
                s.Id,
                s.Name,
                s.Code,
                s.City!.Name,
                string.Join(",", s.Lines!.Select(l => l.Name))))
            
            .ToListAsync();
    }
}