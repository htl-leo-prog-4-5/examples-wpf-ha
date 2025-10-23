using Core.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Entities;

namespace WebUi.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork         _uow;

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await Task.CompletedTask;
        return Page();
    }
}