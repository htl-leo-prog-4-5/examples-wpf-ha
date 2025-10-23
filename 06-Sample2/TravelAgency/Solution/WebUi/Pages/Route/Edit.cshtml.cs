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

namespace WebUi.Pages.Route
{
    using Core.Contracts;

    using Route = Core.Entities.Route;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
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

            var route =  await _uow.RouteRepository.GetByIdAsync(id.Value);
            if (route == null)
            {
                return NotFound();
            }
            Route = route;
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

            _uow.RouteRepository.Attach(Route).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RouteExists(Route.Id))
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

        private async Task<bool> RouteExists(int id)
        {
            return await _uow.RouteRepository.ExistsAsync(id);
        }
    }
}
