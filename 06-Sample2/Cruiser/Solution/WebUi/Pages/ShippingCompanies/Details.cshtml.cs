using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Persistence;

namespace WebUi.Pages.ShippingCompanies
{
    using Core.Contracts;

    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DetailsModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ShippingCompany ShippingCompany { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingcompany = await _uow.ShippingCompanyRepository.GetByIdAsync(id.Value);
            if (shippingcompany == null)
            {
                return NotFound();
            }
            else
            {
                ShippingCompany = shippingcompany;
            }
            return Page();
        }
    }
}
