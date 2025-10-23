#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Contracts;

namespace WebApp.Pages.Customers;

public class EditModel : PageModel
{
    private readonly IUnitOfWork _uow;

    public EditModel(IUnitOfWork uow)
    {
        _uow = uow;
    }


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return Page();
    }
}