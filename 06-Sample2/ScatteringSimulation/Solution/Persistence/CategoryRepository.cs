using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}