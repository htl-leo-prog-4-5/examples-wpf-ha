using Microsoft.EntityFrameworkCore;

using Core.Contracts;
using Core.Entities;
using Core.DataTransferObjects;

using Base.Persistence;

namespace Persistence;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RoomDto>> GetRoomWithBookingsAsync(RoomType? roomType, string? filterNumber)
    {
        IQueryable<Room> rooms = _dbContext.Rooms;
        if (roomType != null)
        {
            rooms = rooms.Where(r => r.RoomType == roomType);
        }

        if (filterNumber != null)
        {
            rooms = rooms.Where(r => r.RoomNumber.StartsWith("R-" + filterNumber));
        }

        var today = DateTime.Now;
        var result = await rooms
            .Select(r => new RoomDto
            {
                RoomId         = r.Id,
                RoomType       = r.RoomType,
                RoomNumber     = r.RoomNumber,
                CurrentBooking = r.Bookings.SingleOrDefault(b => b.From <= today && (b.To == null || b.To >= today))
            }).ToListAsync();
        return result;
    }
}