#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;
using Core.Contracts;
using Core.DataTransferObjects;

namespace WebApp.Pages.Customers
{

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [BindProperty]
        public string FilterName { get; set; }

        [BindProperty]
        public bool OnlyWithCurrentBookings { get; set; }

        public IList<CustomerDto> Customers { get;set; }


        public async Task OnGetAsync()
        {
            Customers = await _uow.Customers.GetAllAsync(null, null);

        }

        public async Task OnPostAsync()
        {
            Customers = await _uow.Customers.GetAllAsync(FilterName, OnlyWithCurrentBookings ? true : null);
        }
    }
}
