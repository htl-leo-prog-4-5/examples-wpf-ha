using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class BacklogItemRepository : GenericRepository<BacklogItem>, IBacklogItemRepository
{
    public BacklogItemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<BacklogItemOverview>> GetBacklogAsync(string? teamMemberName)
    {
        IQueryable<BacklogItem> query = DbSet
            .Include(h => h.TeamMembers)
            .Include(h => h.Comments)
            .Include(h => h.Effort);

        if (!string.IsNullOrEmpty(teamMemberName))
        {
            query = query.Where(h => h.TeamMembers!.Any(c => c.Name == teamMemberName));
        }

        return await query
            .OrderBy(item => item.CreationDate)
            .Select(h => new BacklogItemOverview(
                h.Id,
                h.Name,
                h.Description,
                h.CreationDate,
                h.Priority,
                h.Effort!.Description,
                string.Join(", ", h.Comments!
                    .Select(hl => hl.SeqNo + ": " + hl.Description)),
                string.Join(", ", h.TeamMembers!
                    .Select(c => c.Name))
            ))
            .ToListAsync();
    }
}