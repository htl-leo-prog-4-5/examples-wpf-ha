using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Microsoft.EntityFrameworkCore;

public class OriginRepository : GenericRepository<Origin>, IOriginRepository
{
    public OriginRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Origin?> GetByNameAsync(string? origin)
    {
        if (string.IsNullOrEmpty(origin))
        {
            return null;
        }

        return await DbSet.SingleOrDefaultAsync(o => o.Name == origin);
    }
}