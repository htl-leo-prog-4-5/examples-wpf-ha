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

    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

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
    }
}
