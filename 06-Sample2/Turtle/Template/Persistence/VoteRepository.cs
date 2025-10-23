using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class VoteRepository : GenericRepository<Vote>, IVoteRepository
{
    public VoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}