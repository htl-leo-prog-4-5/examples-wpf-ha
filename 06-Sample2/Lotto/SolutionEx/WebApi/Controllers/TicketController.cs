using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core;
using Core.Entities;

using System.Linq.Expressions;

/// <summary>
/// REST Controller for Ticket`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private readonly IUnitOfWork               _uow;
    private readonly ILogger<TicketController> _logger;

    /// <summary>
    /// Constructor of TicketController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public TicketController(IUnitOfWork uow, ILogger<TicketController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// TicketDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ticketNo"></param>
    /// <param name="tips"></param>
    public record TicketDto(int Id, string ticketNo, IEnumerable<IEnumerable<byte>> tips);

    TicketDto? ToDto(Ticket? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new TicketDto(entity.Id, entity.TicketNo, entity.Tips!.Select(tip => tip.Normalize()));
    }

    IList<TicketDto>? ToDto(IList<Ticket>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    #endregion

    #region default REST

    /// <summary>
    /// Get all Tickets.
    /// </summary>
    /// <param name="ticketNo">The ticketNo of the Ticket.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAsync(string? ticketNo)
    {
        Expression<Func<Ticket, bool>>? filter = null;

        if (!string.IsNullOrEmpty(ticketNo))
        {
            filter = x => x.TicketNo == ticketNo;
        }

        var tickets = await _uow.TicketRepository.GetNoTrackingAsync(filter, null, nameof(Ticket.Tips), nameof(Ticket.Game));

        return await this.NotFoundOrOk(ToDto(tickets));
    }

    /// <summary>
    /// Get a specified Ticket.
    /// </summary>
    /// <param name="id">The id of the Ticket.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDto>> GetAsync(int id)
    {
        var entity = await _uow.TicketRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    #endregion
}