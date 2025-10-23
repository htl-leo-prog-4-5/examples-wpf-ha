namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ITripRepository      TripRepository      { get; }
}