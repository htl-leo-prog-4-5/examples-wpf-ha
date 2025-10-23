using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class MoveRepository : GenericRepository<Move>, IMoveRepository
{
    public MoveRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}