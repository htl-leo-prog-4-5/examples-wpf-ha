namespace Core.Contracts;

using System.Threading.Tasks;

public interface IImportService
{
    Task ImportScriptAsync(string name, string? description, string? origin, DateOnly creationDate, string fileName);
}