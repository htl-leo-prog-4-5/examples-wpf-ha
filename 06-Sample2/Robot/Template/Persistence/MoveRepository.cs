namespace Persistence;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

public class MoveRepository : GenericRepository<Move>, IMoveRepository
{
    public MoveRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}