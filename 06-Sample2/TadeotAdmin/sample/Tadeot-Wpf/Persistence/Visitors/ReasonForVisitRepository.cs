using AuthenticationBase.Persistence.Repositories;
using Core.Contracts.Visitors;
using Core.Entities.Visitors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Visitors
{
    internal class ReasonForVisitRepository : GenericRepository<ReasonForVisit>, IReasonForVisitRepository
    {
        public ApplicationDbContext? DbContext { get; }

        public ReasonForVisitRepository(DbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext as ApplicationDbContext;
        }

        public async Task UpdateAllAsync(string[] reasons)
        {
            var old = await DbContext.ReasonsForVisit.ToListAsync();
            DbContext.ReasonsForVisit.RemoveRange(old);
            var rank = 1;
            var newReasons = reasons
                .Select(r => new ReasonForVisit
                {
                    Rank = rank++,
                    Reason = r
                })
                .ToList();
            await DbContext.ReasonsForVisit.AddRangeAsync(newReasons);
        }
    }
}
