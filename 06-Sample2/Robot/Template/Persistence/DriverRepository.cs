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
}