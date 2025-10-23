using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IGameRepository : IGenericRepository<Game>
{
    Task<Game?> GetByDateAsync(DateOnly fromDate, DateOnly toDate);
    Task<IList<Game>> GetCurrentOpenGamesAsync(DateOnly date);
}