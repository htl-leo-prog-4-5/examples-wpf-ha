using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IExaminationRepository : IGenericRepository<Examination>
{
    Task<Examination?> GetExaminationAsync(string svNumber, DateTime examinationDate);
}