namespace Persistence;

using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Driver?> GetByNameAsync(string driverName)
    {
        return await DbSet.SingleOrDefaultAsync(d => d.Name == driverName);
    }
}