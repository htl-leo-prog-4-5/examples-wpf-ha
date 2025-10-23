using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class ShipRepository : GenericRepository<Ship>, IShipRepository
{
    public ShipRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}