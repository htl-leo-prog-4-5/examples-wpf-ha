using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class GameRepository : GenericRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}