using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Persistence.ImportData;

public class GameRepository : GenericRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Game?> GetByDateAsync(DateOnly fromDate, DateOnly toDate)
    {
        return await DbSet.SingleOrDefaultAsync(game => game.DateFrom == fromDate && game.DateTo == toDate);
    }
}