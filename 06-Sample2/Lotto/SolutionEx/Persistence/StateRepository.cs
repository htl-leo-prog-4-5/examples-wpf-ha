using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Base.Persistence;

public class StateRepository : GenericRepository<State>, IStateRepository
{
    public StateRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}