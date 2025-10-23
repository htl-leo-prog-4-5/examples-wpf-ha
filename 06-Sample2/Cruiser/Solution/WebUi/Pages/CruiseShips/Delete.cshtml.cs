using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.CruiseShips
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
        public CruiseShip CruiseShip { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cruiseship = await _uow.CruiseShipRepository.GetByIdAsync(id.Value,includeProperties:nameof(ShippingCompany));

            if (cruiseship == null)
            {
                return NotFound();
            }
            else
            {
                CruiseShip = cruiseship;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cruiseship = await _uow.CruiseShipRepository.GetByIdAsync(id.Value);
            if (cruiseship != null)
            {
                CruiseShip = cruiseship;
                _uow.CruiseShipRepository.Remove(CruiseShip);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
