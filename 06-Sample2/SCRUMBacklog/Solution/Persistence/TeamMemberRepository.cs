using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
{
    public TeamMemberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}