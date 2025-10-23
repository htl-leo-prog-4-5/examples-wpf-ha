using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.Ship
{
    using Core.Contracts;

    using Ship = Core.Entities.Ship;

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Ship Ship { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _uow.ShipRepository.GetByIdAsync(id.Value);

            if (ship == null)
            {
                return NotFound();
            }
            else
            {
                Ship = ship;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _uow.ShipRepository.GetByIdAsync(id.Value);
            if (ship != null)
            {
                Ship = ship;
                _uow.ShipRepository.Remove(Ship);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
