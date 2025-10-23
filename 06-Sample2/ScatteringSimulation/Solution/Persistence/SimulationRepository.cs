using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

namespace Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

public class SimulationRepository : GenericRepository<Simulation>, ISimulationRepository
{
    public SimulationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<SimulationOverview>> GetSimulationsAsync(string? category)
    {
        IQueryable<Simulation> query = DbSet
            .Include(s => s.Category)
            .Include(s => s.Samples)
            .Include(s => s.Origin);

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(s => s.Category!.Description == category);
        }

        return await query
            .Select(s =>
                new SimulationOverview(
                    s.Id,
                    s.Name,
                    s.CreationDate,
                    s.CategoryId,
                    s.Category!.Description,
                    s.Origin!.Name,
                    s.Samples!.Count()
                ))
            .ToListAsync();
    }
}