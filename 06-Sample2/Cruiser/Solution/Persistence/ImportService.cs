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
        var cruiserCsv = await new CsvImport<CruiserCsv>().ReadAsync("ImportData/Schiffe.txt");

        var companies = cruiserCsv
            .Where(c => !string.IsNullOrEmpty(c.Reederei))
            .Select(c => c.Reederei)
            .Distinct()
            .Select(c => new ShippingCompany { Name = c! })
            .ToDictionary(c => c.Name);

        uint? Convert(decimal? value) => value.HasValue ? (uint?)value.Value : null;

        IList<ShipName> ShipNames(string? remark)
        {
            if (string.IsNullOrEmpty(remark))
            {
                return new List<ShipName>();
            }

            var parts = remark.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Where(s => s.StartsWith("ex ")).Select(s => new ShipName()
            {
                Name = s.Substring("ex ".Length)
            }).ToList();
        }

        string? RemoveShipNames(string? remark)
        {
            if (string.IsNullOrEmpty(remark))
            {
                return null;
            }

            var parts = remark.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var newRemark = string.Join(",", parts.Where(s => !s.StartsWith("ex ")));

            return string.IsNullOrEmpty(newRemark) ? null : newRemark;
        }

        var ships = cruiserCsv
            .Select(c => new CruiseShip()
            {
                Name               = c.Name,
                YearOfConstruction = c.BJ,
                Length             = c.Laenge,
                Tonnage            = Convert(c.BRZ),
                Cabins             = Convert(c.Kab),
                Crew               = Convert(c.Bes),
                Passengers         = Convert(c.Pass),
                ShippingCompany    = string.IsNullOrEmpty(c.Reederei) ? null : companies[c.Reederei],
                Remark             = RemoveShipNames(c.Bemerkungen),
                ShipNames          = ShipNames(c.Bemerkungen)
            })
            .ToList();

        await _uow.CruiseShipRepository.AddRangeAsync(ships);
        await _uow.SaveChangesAsync();
    }
}