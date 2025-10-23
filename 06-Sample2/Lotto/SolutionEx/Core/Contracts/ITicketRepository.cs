using Base.Core.Contracts;

using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<Ticket?> GetTicketAsync(string ticketNo);
}