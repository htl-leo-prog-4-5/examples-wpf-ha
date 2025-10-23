using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Core.Entities;

namespace WebUi.Pages.Stations
{
    using Core.Contracts;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
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

            var station = await _uow.StationRepository.GetByIdAsync(id.Value);
            if (station == null)
            {
                return NotFound();
            }

            Station            = station;
            ViewData["CityId"] = new SelectList(await _uow.CityRepository.GetAsync(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var station = await _uow.StationRepository.GetByIdAsync(Station.Id, includeProperties: nameof(Core.Entities.Station.City));
            if (station == null)
            {
                return NotFound();
            }

            station.Code        = Station.Code;
            station.Name        = Station.Name;
            station.Type        = Station.Type;
            station.StateCode   = Station.StateCode;
            station.IsRegional  = Station.IsRegional;
            station.IsExpress   = Station.IsExpress;
            station.IsIntercity = Station.IsIntercity;
            station.Remark      = Station.Remark;
            station.CityId      = station.CityId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StationExists(Station.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> StationExists(int id)
        {
            return await _uow.StationRepository.ExistsAsync(id);
        }
    }
}