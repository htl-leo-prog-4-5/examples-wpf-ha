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

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<CruiseShip> CruiseShip { get;set; } = default!;

        public async Task OnGetAsync()
        {
            CruiseShip = await _uow.CruiseShipRepository.GetNoTrackingAsync(orderBy: (ships) => ships.OrderBy(s => s.Name), includeProperties: nameof(ShippingCompany));
        }
    }
}
