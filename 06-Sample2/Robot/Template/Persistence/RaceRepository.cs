namespace Persistence;

using System.Linq;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

public class RaceRepository : GenericRepository<Race>, IRaceRepository
{
    public RaceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}