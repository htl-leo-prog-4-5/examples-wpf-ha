namespace WebUi.Pages.CreateTicket;

using System.ComponentModel.DataAnnotations;

using Core;
using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TicketCreatedModel : PageModel
{
    private readonly ILogger<TicketCreatedModel> _logger;
    private readonly IUnitOfWork                 _uow;

    public TicketCreatedModel(ILogger<TicketCreatedModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public Game    CurrentGame = default!;
    public Ticket? Ticket      = default!;

    public async Task<IActionResult> OnGetAsync(string ticketNo)
    {
        Ticket = await _uow.TicketRepository.GetTicketAsync(ticketNo);

        if (Ticket == null)
        {
            return NotFound();
        }

        CurrentGame = Ticket.Game!;

        return Page();
    }
}