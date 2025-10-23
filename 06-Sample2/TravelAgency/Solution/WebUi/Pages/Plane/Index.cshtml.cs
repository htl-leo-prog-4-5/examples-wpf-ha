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

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<Plane> Plane { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Plane = await _uow.PlaneRepository.GetAsync();
        }
    }
}
