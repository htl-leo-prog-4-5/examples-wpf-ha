using Microsoft.EntityFrameworkCore;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

internal class PupilRepository : GenericRepository<Pupil>, IPupilRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PupilRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<Pupil[]> GetAllAsync()
    {
        return _dbContext.Pupils.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToArrayAsync();
    }
}