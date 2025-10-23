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

namespace WebUi.Pages.Hotel
{
    using Core.Contracts;

    using Hotel = Core.Entities.Hotel;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Hotel Hotel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel =  await _uow.HotelRepository.GetByIdAsync(id.Value);
            if (hotel == null)
            {
                return NotFound();
            }
            Hotel = hotel;
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

            _uow.HotelRepository.Attach(Hotel).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(Hotel.Id))
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

        private async Task<bool> HotelExists(int id)
        {
            return await _uow.HotelRepository.ExistsAsync(id);
        }
    }
}
