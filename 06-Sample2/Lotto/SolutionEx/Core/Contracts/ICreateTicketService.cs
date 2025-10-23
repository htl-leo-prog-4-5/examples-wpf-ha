using Core.DataTransferObjects;

namespace Core.Contracts;

public interface ICreateTicketService
{
    Task<string?> CreateTicket(CreateTicketDto dto);
}