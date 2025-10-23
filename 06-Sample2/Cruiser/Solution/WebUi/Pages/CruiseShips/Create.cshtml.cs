using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Core.Entities;

using Persistence;

namespace WebUi.Pages.CruiseShips
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
            ViewData["ShippingCompanyId"] = new SelectList(await _uow.ShippingCompanyRepository.GetNoTrackingAsync(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public CruiseShip CruiseShip { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _uow.CruiseShipRepository.AddAsync(CruiseShip);
            await _uow.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}