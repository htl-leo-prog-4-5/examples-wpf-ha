using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Entities;

namespace WebUi.Pages.Stations
{
    using Core.Contracts;

    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Station Station { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _uow.StationRepository.GetByIdAsync(id.Value,
                nameof(Core.Entities.Station.City),
                nameof(Core.Entities.Station.Infrastructures),
                nameof(Core.Entities.Station.Lines),
                nameof(Core.Entities.Station.RailwayCompanies));

            if (station == null)
            {
                return NotFound();
            }
            else
            {
                Station = station;
            }

            return Page();
        }
    }
}