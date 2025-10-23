using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class HikeRepository : GenericRepository<Hike>, IHikeRepository
{
    public HikeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}