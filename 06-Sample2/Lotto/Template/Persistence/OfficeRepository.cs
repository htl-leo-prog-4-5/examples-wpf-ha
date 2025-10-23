using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
{
    public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}