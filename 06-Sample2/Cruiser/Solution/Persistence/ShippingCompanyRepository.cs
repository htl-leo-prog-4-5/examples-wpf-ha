using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ShippingCompanyRepository : GenericRepository<ShippingCompany>, IShippingCompanyRepository
{
    public ShippingCompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<CompanyOverview>> GetOverviewAsync()
    {
        var list = new List<CompanyOverview>
        {
            new CompanyOverview(null,
                "<No Company>",
                (uint)await Context.Set<CruiseShip>()
                    .Where(c => c.ShippingCompanyId == null)
                    .CountAsync())
        };

        list.AddRange(await DbSet
            .Include(c => c.CruiseShips)
            .OrderBy(c => c.Name)
            .Select(c => new CompanyOverview(c.Id, c.Name, (uint)c.CruiseShips!.Count))
            .ToListAsync());

        return list;
    }
}