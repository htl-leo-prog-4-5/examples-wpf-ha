using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class OriginRepository : GenericRepository<Origin>, IOriginRepository
{
    public OriginRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}