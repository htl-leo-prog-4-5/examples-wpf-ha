namespace WebUi.Pages.CreateTicket;

using System.ComponentModel.DataAnnotations;

using Core;
using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class NewTicketModel : PageModel
{
    private readonly ILogger<NewTicketModel> _logger;
    private readonly IUnitOfWork             _uow;
    private readonly ICreateTicketService    _createTicketService;

    public NewTicketModel(ILogger<NewTicketModel> logger, IUnitOfWork uow, ICreateTicketService createTicketService)
    {
        _logger              = logger;
        _uow                 = uow;
        _createTicketService = createTicketService;
    }

    public Game CurrentGame = default!;

    public record TipDto(
        int Id,
        [Required]
        string officeNo,
        [Range(1, 49)]
        uint no1,
        [Range(1, 49)]
        uint no2,
        [Range(1, 49)]
        uint no3,
        [Range(1, 49)]
        uint no4,
        [Range(1, 49)]
        uint no5,
        [Range(1, 49)]
        uint no6
    );

    [BindProperty]
    public TipDto Tip { get; set; }

    [BindProperty]
    public int GameId { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        GameId      = id;
        CurrentGame = await _uow.GameRepository.GetByIdAsync(id);

        if (CurrentGame == null)
        {
            return NotFound();
        }

        ViewData["Offices"] = new SelectList(await _uow.OfficeRepository.GetAsync(), "No", "Name");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ViewData["Offices"] = new SelectList(await _uow.OfficeRepository.GetAsync(), "No", "Name");
        CurrentGame         = (await _uow.GameRepository.GetByIdAsync(GameId))!;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var tip = TipExtensions.ToTip(Tip.no1, Tip.no2, Tip.no3, Tip.no4, Tip.no5, Tip.no6).ToTip();


        if (tip.Normalize().Distinct().ToArray().Length != 6)
        {
            ModelState.AddModelError(string.Empty, "Tip must have 6 unique numbers");
            return Page();
        }


        var ticketNo = await _createTicketService.CreateTicket(new CreateTicketDto(
            Tip.officeNo,
            CurrentGame.DateFrom,
            CurrentGame.DateTo,
            new List<IEnumerable<uint>>() { tip.Normalize().Select(no => (uint) no) })
        );

        return RedirectToPage("./TicketCreated", new {ticketNo = ticketNo});
    }
}