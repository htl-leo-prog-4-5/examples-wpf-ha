using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class DifficultyRepository : GenericRepository<Difficulty>, IDifficultyRepository
{
    public DifficultyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}