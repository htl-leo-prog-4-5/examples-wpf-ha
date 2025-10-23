using Core.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages;

using Core.Entities;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork         _uow;

    public IList<Office> Offices { get; set; } = new List<Office>();

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Offices = await _uow.OfficeRepository.GetAsync();

        return Page();
    }
}