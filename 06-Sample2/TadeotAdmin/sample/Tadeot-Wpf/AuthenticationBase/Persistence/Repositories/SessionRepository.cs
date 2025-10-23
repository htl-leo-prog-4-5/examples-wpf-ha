using System.Linq;
using System.Threading.Tasks;

using AuthenticationBase.Contracts.Persistence;
using AuthenticationBase.Entities;

using Microsoft.EntityFrameworkCore;

namespace AuthenticationBase.Persistence.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly BaseApplicationDbContext _dbContext;

        public SessionRepository(BaseApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session?> GetLastByUserAsync(string userId)
        {
            return await _dbContext.Sessions
                    .Where(s => s.ApplicationUserId == userId)
                    .OrderByDescending(s => s.Login)
                    .FirstOrDefaultAsync();
        }

    }
}
