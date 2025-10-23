namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;
using System;

using Base.Tools;
using Base.Tools.CsvImport;

using Core;
using Core.DataTransferObjects;

using Persistence.ImportData;

public class CreateTicketService : ICreateTicketService
{
    private IUnitOfWork _uow;

    public CreateTicketService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string?> CreateTicket(CreateTicketDto dto)
    {
        var game   = await _uow.GameRepository.GetByDateAsync(dto.gameFromDate, dto.gameToDate);
        var office = await _uow.OfficeRepository.GetByNoAsync(dto.OfficeNo);

        if (game is null ||
            office is null ||
            game.DrawDate.HasValue
           )
        {
            return null;
        }

        var tips = dto.tips
            .Select(tip => tip.Select(no => (byte)no).ToArray().Normalize().ToArray())
            .Where(tip => tip.Length == 6 && !tip.Any(no => no > 45 || no < 1))
            .Select(tip => new Tip()
            {
                No1 = tip[0],
                No2 = tip[1],
                No3 = tip[2],
                No4 = tip[3],
                No5 = tip[4],
                No6 = tip[5],
            })
            .ToList();

        var ticketNo = Guid.NewGuid().ToString();

        var ticket = new Ticket()
        {
            Game     = game,
            Office   = office,
            Created  = DateTime.Now,
            TicketNo = ticketNo,
            Tips     = tips
        };
        await _uow.TicketRepository.AddAsync(ticket);
        await _uow.SaveChangesAsync();

        return ticketNo;
    }
}