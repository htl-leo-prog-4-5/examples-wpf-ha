namespace WebUi.Pages.CreateTicket;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork         _uow;

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public IList<Game> CurrentGames = default!;
    
    public async Task<IActionResult> OnGetAsync()
    {
        CurrentGames = await _uow.GameRepository.GetCurrentOpenGamesAsync(DateOnly.FromDateTime(DateTime.Today));
        return Page();
    }
}