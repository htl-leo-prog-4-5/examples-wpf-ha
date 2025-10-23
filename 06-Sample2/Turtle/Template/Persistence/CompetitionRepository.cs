using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}