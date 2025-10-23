#nullable disable
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
using Core.Contracts;

namespace WebApp.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public List<Booking> Bookings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _uow.Customers.GetByIdAsync(id.Value);
            Bookings = await _uow.Bookings.GetBookingsForCustomer(id.Value);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Bookings = await _uow.Bookings.GetBookingsForCustomer(Customer.Id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var dbCustomer = await _uow.Customers.GetByIdAsync(Customer.Id);
            dbCustomer.FirstName = Customer.FirstName;
            dbCustomer.LastName = Customer.LastName;
            dbCustomer.EmailAddress = Customer.EmailAddress;
            dbCustomer.CreditCardNumber = Customer.CreditCardNumber;
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException.Message);
                Customer = await _uow.Customers.GetByIdAsync(Customer.Id);
            }

            return Page();
        }


    }
}
