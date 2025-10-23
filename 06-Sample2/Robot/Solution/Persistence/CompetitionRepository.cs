namespace Persistence;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;
using Core.QueryResults;

using Microsoft.EntityFrameworkCore;

public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Competition?> GetByNameAsync(string competitionName)
    {
        return await DbSet.SingleOrDefaultAsync(d => d.Name == competitionName);
    }

    public async Task<IList<Competition>> GetAllWithRaceAsync()
    {
        return await DbSet
            .Include(c => c.Races)
            .Where(c => c.Races!.Count > 0)
            .ToListAsync();
    }

    public async Task<IList<CompetitionSummary>> GetSummaryAsync()
    {
        return await DbSet
            .Include(c => c.Races)
            .Select(c => new CompetitionSummary()
            {
                Id        = c.Id,
                Name      = c.Name,
                RaceCount = c.Races!.Count,
                FirstRace = c.Races.Min(r => r.RaceStartTime),
                LastRace  = c.Races.Max(r => r.RaceStartTime),
            })
            .ToListAsync();
    }
}