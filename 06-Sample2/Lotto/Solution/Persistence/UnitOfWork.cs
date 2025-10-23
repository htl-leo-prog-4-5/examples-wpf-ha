using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;
using Base.Tools;

using Persistence.ImportData;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ITipRepository    TipRepository    { get; }
    public IOfficeRepository OfficeRepository { get; }
    public ITicketRepository TicketRepository { get; }
    public IGameRepository   GameRepository   { get; }

    public UnitOfWork(ApplicationDbContext context,
        ITipRepository                     tipRepository,
        IOfficeRepository                  officeRepository,
        ITicketRepository                  ticketRepository,
        IGameRepository                    gameRepository
    ) : base(context)
    {
        TipRepository    = tipRepository;
        OfficeRepository = officeRepository;
        TicketRepository = ticketRepository;
        GameRepository   = gameRepository;
    }
}