using AuthenticationBase.Persistence.Repositories;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

namespace Persistence.Visitors;

public class DistrictRepository : GenericRepository<District>, IDistrictRepository
{
    public ApplicationDbContext? DbContext { get; }

    public DistrictRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}