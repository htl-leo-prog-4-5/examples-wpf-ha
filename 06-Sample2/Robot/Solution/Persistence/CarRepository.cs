namespace Persistence;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

public class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Car?> GetByNameAsync(string carName)
    {
        return await DbSet.SingleOrDefaultAsync(d => d.Name == carName);
    }
}