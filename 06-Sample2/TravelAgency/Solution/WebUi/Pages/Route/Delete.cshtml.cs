using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.Route
{
    using Core.Contracts;

    using Route = Core.Entities.Route;

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Route Route { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _uow.RouteRepository.GetByIdAsync(id.Value);

            if (route == null)
            {
                return NotFound();
            }
            else
            {
                Route = route;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _uow.RouteRepository.GetByIdAsync(id.Value);
            if (route != null)
            {
                Route = route;
                _uow.RouteRepository.Remove(Route);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
