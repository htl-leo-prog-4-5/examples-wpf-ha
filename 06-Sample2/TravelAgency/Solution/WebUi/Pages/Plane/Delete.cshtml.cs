using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.Plane
{
    using Core.Contracts;

    using Plane = Core.Entities.Plane;

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
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

            var plane = await _uow.PlaneRepository.GetByIdAsync(id.Value);

            if (plane == null)
            {
                return NotFound();
            }
            else
            {
                Plane = plane;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _uow.PlaneRepository.GetByIdAsync(id.Value);
            if (plane != null)
            {
                Plane = plane;
                _uow.PlaneRepository.Remove(Plane);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
