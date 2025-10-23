using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class InfrastructureRepository : GenericRepository<Infrastructure>, IInfrastructureRepository
{
    public InfrastructureRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}