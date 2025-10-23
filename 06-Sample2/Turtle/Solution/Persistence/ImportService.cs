namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportScriptAsync(string name, string? description, string? origin, DateOnly creationDate, string fileName)
    {
        string allLines = await File.ReadAllTextAsync(fileName);

        if (string.IsNullOrEmpty(allLines))
        {
            return;
        }

        var moves = new List<Move>();

        const string validDirections = "ABCDEFGH";

        int lastCount = 0;
        int lastColor = 0;
        int seqNo     = 1;

        foreach (var ch in allLines)
        {
            int idx = validDirections.IndexOf(char.ToUpper(ch));

            if (idx >= 0)
            {
                moves.Add(
                    new Move()
                    {
                        SeqNo     = seqNo++,
                        Color     = char.IsUpper(ch) ? lastColor : null,
                        Repeat    = lastCount > 0 ? lastCount : 1,
                        Direction = idx,
                    });

                lastCount = 0;
            }
            else if (char.IsDigit(ch))
            {
                lastCount = lastCount * 10 + (ch - '0');
            }
            else if (ch == '$')
            {
                lastColor = lastCount;
                lastCount = 0;
            }
            else
            {
                lastCount = 0;
            }
        }

        var script = new Script()
        {
            Name         = name,
            Description  = description,
            CreationDate = creationDate,
            Origin = (await _uow.OriginRepository.GetByNameAsync(origin)) ??
                     new Origin() { Code = origin ?? "unknown", Name = origin ?? "unknown" },
            Moves = moves
        };

        await _uow.ScriptRepository.AddAsync(script);
        await _uow.SaveChangesAsync();
    }
}