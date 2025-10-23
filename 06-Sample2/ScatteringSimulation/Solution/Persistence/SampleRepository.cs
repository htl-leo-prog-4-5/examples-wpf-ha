using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class SampleRepository : GenericRepository<Sample>, ISampleRepository
{
    public SampleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}