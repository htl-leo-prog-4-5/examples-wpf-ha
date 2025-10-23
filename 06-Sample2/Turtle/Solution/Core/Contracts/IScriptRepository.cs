using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IScriptRepository : IGenericRepository<Script>
{
    Task<IList<ScriptOverview>> GetScriptOverviewAsync(string? category);
}