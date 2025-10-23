using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class RailwayCompanyRepository : GenericRepository<RailwayCompany>, IRailwayCompanyRepository
{
    public RailwayCompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}