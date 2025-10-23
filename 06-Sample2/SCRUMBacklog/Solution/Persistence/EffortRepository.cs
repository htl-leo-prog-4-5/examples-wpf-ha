using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class EffortRepository : GenericRepository<Effort>, IEffortRepository
{
    public EffortRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}