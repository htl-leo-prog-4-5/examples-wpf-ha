using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Entities;

namespace WebUi.Pages.Stations
{
    using Core.Contracts;

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Station Station { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _uow.StationRepository.GetByIdAsync(id.Value, nameof(Core.Entities.Station.City));

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _uow.StationRepository.GetByIdAsync(id.Value, nameof(Core.Entities.Station.City));
            if (station != null)
            {
                Station = station;
                _uow.StationRepository.Remove(Station);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
