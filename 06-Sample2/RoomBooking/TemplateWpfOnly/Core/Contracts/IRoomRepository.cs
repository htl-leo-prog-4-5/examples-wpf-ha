using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IRoomRepository : IGenericRepository<Room>
{
    Task<List<RoomDto>> GetRoomWithBookingsAsync(RoomType? roomType, string? filterNumber);
}