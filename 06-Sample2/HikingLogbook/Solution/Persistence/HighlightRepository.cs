using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class HighlightRepository : GenericRepository<Highlight>, IHighlightRepository
{
    public HighlightRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}