using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class TipRepository : GenericRepository<Tip>, ITipRepository
{
    public TipRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}