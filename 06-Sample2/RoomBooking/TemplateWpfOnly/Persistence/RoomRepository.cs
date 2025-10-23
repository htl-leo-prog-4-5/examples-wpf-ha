using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

using Core.DataTransferObjects;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RoomDto>> GetRoomWithBookingsAsync(RoomType? roomType, string? filterNumber)
    {
        //TODO
        return new List<RoomDto>();
    }
  
}