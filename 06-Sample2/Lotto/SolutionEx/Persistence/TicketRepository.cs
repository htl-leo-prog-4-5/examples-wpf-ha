using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Base.Persistence;

using Core;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Ticket?> GetTicketAsync(string ticketNo)
    {
        return await DbSet
            .Include(t => t.Tips)
            .Include(t => t.Game)
            .Include(t => t.Office)
            .Where(t => t.TicketNo == ticketNo)
            .SingleOrDefaultAsync();
    }
}