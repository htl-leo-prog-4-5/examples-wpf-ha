#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Contracts;

namespace WebApp.Pages.Customers;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _uow;

    public IndexModel(IUnitOfWork uow)
    {
        _uow = uow;
    }


    public async Task OnGetAsync()
    {
    }
}