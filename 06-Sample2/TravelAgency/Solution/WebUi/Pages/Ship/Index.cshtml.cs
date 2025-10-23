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

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<Ship> Ship { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ship = await _uow.ShipRepository.GetAsync();
        }
    }
}
