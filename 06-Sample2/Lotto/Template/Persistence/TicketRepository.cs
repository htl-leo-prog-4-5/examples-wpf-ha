using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
