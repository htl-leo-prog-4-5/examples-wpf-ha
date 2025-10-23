using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class BacklogItemRepository : GenericRepository<BacklogItem>, IBacklogItemRepository
{
    public BacklogItemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}