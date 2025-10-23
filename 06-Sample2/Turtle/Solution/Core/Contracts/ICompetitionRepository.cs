using Base.Core.Contracts;

using Core.Entities;

using Core.DataTransferObjects;

namespace Core.Contracts;

public interface ICompetitionRepository : IGenericRepository<Competition>
{
    Task<CompetitionVoteResult?> GetCompetitionResult(int id);
}