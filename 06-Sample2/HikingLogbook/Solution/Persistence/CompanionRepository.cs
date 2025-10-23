using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class CompanionRepository : GenericRepository<Companion>, ICompanionRepository
{
    public CompanionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}