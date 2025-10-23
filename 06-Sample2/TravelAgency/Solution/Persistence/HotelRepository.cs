using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
{
    public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}