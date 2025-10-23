using Core.Entities;

namespace Core.Contracts;

public interface IPupilRepository : IGenericRepository<Pupil>
{
    Task<Pupil[]> GetAllAsync();
}