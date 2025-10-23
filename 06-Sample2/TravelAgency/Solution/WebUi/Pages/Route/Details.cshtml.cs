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

    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Route Route { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _uow.RouteRepository.GetByIdAsync(id.Value,
                nameof(Route.Steps),
                $"{nameof(Route.Steps)}.{nameof(Hotel)}",
                $"{nameof(Route.Steps)}.{nameof(Plane)}",
                $"{nameof(Route.Steps)}.{nameof(Ship)}");
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
    }
}