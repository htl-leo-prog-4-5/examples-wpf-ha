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

namespace WebUi.Pages.Plane
{
    using Core.Contracts;

    using Plane = Core.Entities.Plane;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Plane Plane { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane =  await _uow.PlaneRepository.GetByIdAsync(id.Value);
            if (plane == null)
            {
                return NotFound();
            }
            Plane = plane;
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

            _uow.PlaneRepository.Attach(Plane).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PlaneExists(Plane.Id))
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

        private async Task<bool> PlaneExists(int id)
        {
            return await _uow.PlaneRepository.ExistsAsync(id);
        }
    }
}
