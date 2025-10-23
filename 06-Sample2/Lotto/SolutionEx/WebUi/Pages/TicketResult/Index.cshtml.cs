namespace WebUi.Pages.TicketResult;

using Core;
using Core.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork         _uow;

    [BindProperty]
    public string TicketNo { get; set; } = string.Empty;

    public record TicketResult(int idx, string count);

    public IList<TicketResult> TicketResults { get; set; } = new List<TicketResult>();

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ticket = await _uow.TicketRepository.GetTicketAsync(TicketNo);

        if (ticket == null)
        {
            ModelState.AddModelError(nameof(TicketNo), "Ticket not found");
            return Page();
        }

        var gameResult   = ticket.Game!.GetResult();
        var gameResultZZ = ticket.Game!.GetResultZZ();

        if (gameResult.Count == 0)
        {
            ModelState.AddModelError(nameof(TicketNo), "Game not finished");
            return Page();
        }

        foreach (var tip in ticket.Tips!)
        {
            var sameNos = tip.SameNos(gameResult);
            if (sameNos.Count >= 3)
            {
                TicketResults.Add(new TicketResult(tip.Id, sameNos.Count.ToString()));
            }
        }

        return Page();
    }
}