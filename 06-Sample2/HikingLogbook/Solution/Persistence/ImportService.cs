namespace Persistence;

using System.Collections;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;

using Base.Tools.CsvImport;

using Persistence.ImportData;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportDbAsync()
    {
        var hikeCsv = await (new CsvImport<HikeCsv>().ReadAsync("ImportData/MyHikes.txt"));

        var difficulties = new[]
            {
                "Easy",
                "Moderate",
                "Strenuous"
            }.Select(s => new Difficulty()
            {
                Description = s
            })
            .ToList();


        var allCompanions = (await _uow.CompanionRepository.GetAsync()).ToList();

        IList<Companion> GetCompanions(string companions)
        {
            var companionNames = companions
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            var missingCompanions = companionNames
                .Select(c => new Companion() { Name = c })
                .ExceptBy(allCompanions.Select(m => m.Name), m => m.Name)
                .ToList();

            allCompanions.AddRange(missingCompanions);

            return allCompanions
                .Where(c => companionNames.Contains(c.Name))
                .ToList();
        }

        var hike = hikeCsv.Select(h =>
            new Hike()
            {
                Date       = h.Date,
                Distance   = h.Distance,
                Trail      = h.Trail,
                Location   = h.Location,
                Duration   = h.Duration,
                Difficulty = difficulties.Single(d => d.Description == h.Difficulty),
                Highlights = h.Highlights
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(hl => new Highlight() { Description = hl.Trim() }).ToList(),
                Companions = GetCompanions(h.Companions)
            });

        await _uow.DifficultyRepository.AddRangeAsync(difficulties);
        await _uow.HikeRepository.AddRangeAsync(hike);
        await _uow.SaveChangesAsync();
    }
}