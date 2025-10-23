using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Core.Entities;

using Persistence;

namespace WebUi.Pages.ShippingCompanies
{
    using Core.Contracts;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
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

            ShippingCompany = shippingcompany;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var shippingcompany = await _uow.ShippingCompanyRepository.GetByIdAsync(ShippingCompany.Id);
            if (shippingcompany == null)
            {
                return NotFound();
            }

            shippingcompany.Name     = ShippingCompany.Name;
            shippingcompany.City     = ShippingCompany.City;
            shippingcompany.PLZ      = ShippingCompany.PLZ;
            shippingcompany.Street   = ShippingCompany.Street;
            shippingcompany.StreetNo = ShippingCompany.StreetNo;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShippingCompanyExists(ShippingCompany.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ShippingCompanyExists(int id)
        {
            return await _uow.ShippingCompanyRepository.ExistsAsync(id);
        }
    }
}