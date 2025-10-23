namespace Core.DataTransferObjects;

using Core.Entities;

public record CreateTicketDto(string OfficeNo, DateOnly gameFromDate, DateOnly gameToDate, IEnumerable<IEnumerable<uint>> tips);
