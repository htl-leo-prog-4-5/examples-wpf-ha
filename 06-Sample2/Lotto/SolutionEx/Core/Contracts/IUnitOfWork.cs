namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ITipRepository    TipRepository    { get; }
    public ITicketRepository TicketRepository { get; }
    public IOfficeRepository OfficeRepository { get; }
    public IGameRepository   GameRepository   { get; }
    public IStateRepository  StateRepository  { get; }
}