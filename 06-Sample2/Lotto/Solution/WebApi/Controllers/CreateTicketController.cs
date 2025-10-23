using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.DataTransferObjects;
using Core.Entities;

/// <summary>
/// REST Controller for Ticket`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CreateTicketController : ControllerBase
{
    private readonly IUnitOfWork                     _uow;
    private readonly ICreateTicketService            _createTicketService;
    private readonly ILogger<CreateTicketController> _logger;

    /// <summary>
    /// Constructor of OfficeController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public CreateTicketController(IUnitOfWork uow, ICreateTicketService createTicketService ,ILogger<CreateTicketController> logger)
    {
        _uow                      = uow;
        _createTicketService = createTicketService;
        _logger                   = logger;
    }


    #region default REST

    /// <summary>
    /// Add a new Ticket to the database.
    /// </summary>
    /// <param name="value">Values of the new Ticket.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateTicketDto>> AddAsync([FromBody] CreateTicketDto value)
    {
        return Ok(await _createTicketService.CreateTicket(value));
    }

    #endregion
}