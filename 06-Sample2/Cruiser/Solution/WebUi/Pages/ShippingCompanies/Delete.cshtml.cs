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

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingcompany = await _uow.ShippingCompanyRepository.GetByIdAsync(id.Value);
            if (shippingcompany != null)
            {
                ShippingCompany = shippingcompany;
                _uow.ShippingCompanyRepository.Remove(ShippingCompany);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
