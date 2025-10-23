using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.CruiseShips
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
        public CruiseShip CruiseShip { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cruiseship =  await _uow.CruiseShipRepository.GetByIdAsync(id.Value, includeProperties: nameof(ShippingCompany));
            if (cruiseship == null)
            {
                return NotFound();
            }
            CruiseShip                   = cruiseship;
           ViewData["ShippingCompanyId"] = new SelectList(await _uow.CruiseShipRepository.GetAsync(), "Id", "Name");
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

            var cruiseship = await _uow.CruiseShipRepository.GetByIdAsync(CruiseShip.Id, includeProperties: nameof(ShippingCompany));
            if (cruiseship == null)
            {
                return NotFound();
            }

            cruiseship.ShippingCompanyId  = CruiseShip.ShippingCompanyId;
            cruiseship.Name               = CruiseShip.Name;
            cruiseship.YearOfConstruction = CruiseShip.YearOfConstruction;
            cruiseship.Tonnage            = CruiseShip.Tonnage;
            cruiseship.Length             = CruiseShip.Length;
            cruiseship.Cabins             = CruiseShip.Cabins;
            cruiseship.Passengers         = CruiseShip.Passengers;
            cruiseship.Crew               = CruiseShip.Crew;
            cruiseship.Remark             = CruiseShip.Remark;
            cruiseship.ShippingCompanyId  = CruiseShip.ShippingCompanyId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CruiseShipExists(CruiseShip.Id))
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

        private async Task<bool> CruiseShipExists(int id)
        {
            return await _uow.CruiseShipRepository.ExistsAsync(id);
        }
    }
}
