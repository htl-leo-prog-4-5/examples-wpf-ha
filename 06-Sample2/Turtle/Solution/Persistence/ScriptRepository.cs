using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

namespace Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

public class ScriptRepository : GenericRepository<Script>, IScriptRepository
{
    public ScriptRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<ScriptOverview>> GetScriptOverviewAsync(string? origin)
    {
        IQueryable<Script> query = DbSet
            .Include(s => s.Competitions)
            .Include(s => s.Moves)
            .Include(s => s.Origin);

        if (!string.IsNullOrEmpty(origin))
        {
            query = query.Where(s => s.Origin!.Name == origin);
        }

        return await query
            .OrderBy(c => c.CreationDate)
            .Select(s =>
                new ScriptOverview(
                    s.Id,
                    s.Name,
                    s.CreationDate,
                    s.OriginId,
                    s.Origin!.Name,
                    s.Moves!.Count(),
                    string.Join(",", s.Competitions!
                        .Select(c => c.Description))
                ))
            
            .ToListAsync();
    }
}