using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

using Core.DataTransferObjects;

using Microsoft.EntityFrameworkCore;

public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CompetitionVoteResult?> GetCompetitionResult(int id)
    {
        var competition = await DbSet
            .SingleOrDefaultAsync(c => c.Id == id);

        if (competition is null)
        {
            return null;
        }

        return
            new CompetitionVoteResult(
                competition.Id,
                competition.Description,
                await Context.Set<Vote>()
                    .Include(v => v.Script)
                    .Where(c => c.CompetitionId == id)
                    .GroupBy(v => v.ScriptId)
                    .Select(grp => new VoteResult
                    (grp.Key,
                        grp.First().Script!.Name,
                        grp.Count(v => v.Rating == 1),
                        grp.Count(v => v.Rating == 2),
                        grp.Count(v => v.Rating == 3),
                        grp.Count(v => v.Rating == 4),
                        grp.Count(v => v.Rating == 5)))
                    .ToListAsync()
            );
    }
}