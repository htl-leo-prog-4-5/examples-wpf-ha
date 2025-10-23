using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.Hotel
{
    using Core.Contracts;

    using Hotel = Core.Entities.Hotel;

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<Hotel> Hotel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Hotel = await _uow.HotelRepository.GetAsync();
        }
    }
}
