namespace Core.Contracts;

using System.Collections;

using Base.Core.Contracts;

using Core.Entities;

using System.Threading.Tasks;
using System.Collections.Generic;

using Core.QueryResults;

public interface ICompetitionRepository : IGenericRepository<Competition>
{
    Task<Competition?> GetByNameAsync(string competitionName);

    Task<IList<Competition>> GetAllWithRaceAsync();

    Task<IList<CompetitionSummary>> GetSummaryAsync();
}