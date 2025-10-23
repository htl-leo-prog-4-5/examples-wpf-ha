namespace Core.Contracts;

using Base.Core.Contracts;

using Core.Entities;

using System.Threading.Tasks;

public interface ICarRepository : IGenericRepository<Car>
{
    Task<Car?> GetByNameAsync(string carName);
}