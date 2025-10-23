using Base.Persistence;

using Core;
using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class TipRepository : GenericRepository<Tip>, ITipRepository
{
    public TipRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}