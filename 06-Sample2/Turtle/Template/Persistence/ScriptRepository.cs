using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class ScriptRepository : GenericRepository<Script>, IScriptRepository
{
    public ScriptRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}