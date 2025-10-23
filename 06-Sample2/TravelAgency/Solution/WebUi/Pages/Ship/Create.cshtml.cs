using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.Ship
{
    using Core.Contracts;

    using Ship = Core.Entities.Ship;

    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public CreateModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _uow.ShipRepository.AddAsync(Ship);
            await _uow.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
