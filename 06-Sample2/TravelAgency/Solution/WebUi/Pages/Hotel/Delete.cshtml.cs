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

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
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

            var hotel = await _uow.HotelRepository.GetByIdAsync(id.Value);

            if (hotel == null)
            {
                return NotFound();
            }
            else
            {
                Hotel = hotel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _uow.HotelRepository.GetByIdAsync(id.Value);
            if (hotel != null)
            {
                Hotel = hotel;
                _uow.HotelRepository.Remove(Hotel);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
