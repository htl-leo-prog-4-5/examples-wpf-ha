using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class HikeRepository : GenericRepository<Hike>, IHikeRepository
{
    public HikeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<HikeOverview>> GetHikesAsync(string? companion)
    {
        IQueryable<Hike> query = DbSet
            .Include(h => h.Companions)
            .Include(h => h.Highlights)
            .Include(h => h.Difficulty);

        if (!string.IsNullOrEmpty(companion))
        {
            query = query.Where(h => h.Companions!.Any(c => c.Name == companion));
        }

        return await query
            .Select(h => new HikeOverview(
                h.Id,
                h.Trail,
                h.Date,
                h.Location,
                h.Duration,
                h.Distance,
                h.Difficulty!.Description,
                string.Join(",", h.Companions!
                    .Select(c => c.Name)),
                string.Join(",", h.Highlights!
                    .Select(hl => hl.Description))
            ))
            .ToListAsync();
    }
}