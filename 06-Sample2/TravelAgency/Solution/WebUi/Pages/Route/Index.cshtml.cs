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

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<Route> Route { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Route = await _uow.RouteRepository.GetAsync();
        }
    }
}
