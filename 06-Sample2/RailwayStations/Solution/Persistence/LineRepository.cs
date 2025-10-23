using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class LineRepository : GenericRepository<Line>, ILineRepository
{
    public LineRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}