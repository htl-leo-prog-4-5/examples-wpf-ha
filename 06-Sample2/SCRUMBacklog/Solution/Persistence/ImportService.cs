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
        var backlogItemCsvs = await (new CsvImport<BacklogItemCsv>().ReadAsync("ImportData/MyBacklogItems.txt"));

        var efforts = new[]
            {
                "XS",
                "S",
                "M",
                "L",
                "XL"
            }.Select(s => new Effort()
            {
                Description = s
            })
            .ToList();


        var allTeamMembers = (await _uow.TeamMemberRepository.GetAsync()).ToList();

        IList<TeamMember> GetTeamMember(string teamMember)
        {
            var teamMemberNames = teamMember
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            var missingTeamMembers = teamMemberNames
                .Select(c => new TeamMember() { Name = c })
                .ExceptBy(allTeamMembers.Select(m => m.Name), m => m.Name)
                .ToList();

            allTeamMembers.AddRange(missingTeamMembers);

            return allTeamMembers
                .Where(c => teamMemberNames.Contains(c.Name))
                .ToList();
        }

        var backlogItems = backlogItemCsvs.Select(h =>
            new BacklogItem()
            {
                Name         = h.Name,
                Description = h.Description,
                CreationDate = h.Date,
                Priority     = h.Priority,
                Effort       = efforts.SingleOrDefault(d => d.Description == h.Effort),
                Comments = h.Comments
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select((hl,idx) => new Comment() { Description = hl.Trim(), SeqNo = idx+1}).ToList(),
                TeamMembers = GetTeamMember(h.TeamMembers)
            });

        await _uow.EffortRepository.AddRangeAsync(efforts);
        await _uow.BacklogItemRepository.AddRangeAsync(backlogItems);
        await _uow.SaveChangesAsync();
    }
}