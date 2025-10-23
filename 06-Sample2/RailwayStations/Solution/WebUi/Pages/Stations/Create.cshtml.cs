using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Core.Entities;

namespace WebUi.Pages.Stations
{
    using Core.Contracts;

    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public CreateModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["CityId"] = new SelectList(await _uow.CityRepository.GetAsync(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Station Station { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _uow.StationRepository.AddAsync(Station);
            await _uow.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}