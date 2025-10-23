using AuthenticationBase.Persistence.Repositories;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Visitors;

public class ReasonForVisitRepository : GenericRepository<ReasonForVisit>, IReasonForVisitRepository
{
    public ApplicationDbContext? DbContext { get; }

    public ReasonForVisitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }

    public async Task UpdateAllAsync(string[] reasons)
    {
        var old = await DbContext.ReasonsForVisit.ToListAsync();
        DbContext.ReasonsForVisit.RemoveRange(old);
        var rank = 1;
        var newReasons = reasons
            .Select(r => new ReasonForVisit
            {
                Rank   = rank++,
                Reason = r
            })
            .ToList();
        await DbContext.ReasonsForVisit.AddRangeAsync(newReasons);
    }
}