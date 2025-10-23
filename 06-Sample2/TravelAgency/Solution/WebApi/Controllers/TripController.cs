using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

/// <summary>
/// REST Controller for Trip`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TripController : ControllerBase
{
    private readonly IUnitOfWork             _uow;
    private readonly ILogger<TripController> _logger;

    /// <summary>
    /// Constructor of TripController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public TripController(IUnitOfWork uow, ILogger<TripController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// Trip Dto.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="RouteId"></param>
    /// <param name="DepartureDateTime"></param>
    /// <param name="ArrivalDateTime"></param>
    public record TripDto(
        int      Id,
        int      RouteId,
        DateTime DepartureDateTime,
        DateTime ArrivalDateTime,
        bool     Available,
        RouteDto Route
    );

    public record RouteStepDto(
        int    No,
        string Description
    );

    public record RouteDto(
        string         Name,
        RouteStepDto[] Steps
    );

    Trip ToEntity(TripDto dto)
    {
        return new Trip()
        {
            Id                = dto.Id,
            RouteId           = dto.RouteId,
            DepartureDateTime = dto.DepartureDateTime,
            ArrivalDateTime   = dto.ArrivalDateTime
        };
    }

    TripDto? ToDto(Trip? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new TripDto(entity.Id,
            entity.RouteId,
            entity.DepartureDateTime, entity.ArrivalDateTime,
            entity.DepartureDateTime > entity.ArrivalDateTime,
            new RouteDto(entity.Route.Name, entity.Route.Steps.Select(s => new RouteStepDto(s.No, s.Description)).ToArray()));
    }

    IList<TripDto>? ToDto(IList<Trip>? list)
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
    /// Get all Trips.
    /// </summary>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetAsync(string? sort)
    {
        Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderBy =
            sort switch
            {
                nameof(Trip.Id)                => (query) => query.OrderBy(o => o.Id),
                nameof(Trip.RouteId)           => (query) => query.OrderBy(o => o.RouteId),
                nameof(Trip.DepartureDateTime) => (query) => query.OrderBy(o => o.DepartureDateTime),
                nameof(Trip.ArrivalDateTime)   => (query) => query.OrderBy(o => o.ArrivalDateTime),
                _                              => null
            };

        var allEntities = await _uow.TripRepository.GetNoTrackingAsync(null, orderBy, nameof(Trip.Route), $"{nameof(Trip.Route)}.{nameof(Trip.Route.Steps)}");

        return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Trip.
    /// </summary>
    /// <param name="id">The id of the Trip.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TripDto>> GetAsync(int id)
    {
        var entity = await _uow.TripRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    /// <summary>
    /// Add a new Trip to the database.
    /// </summary>
    /// <param name="value">Values of the new Trip.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<TripDto>> AddAsync([FromBody] TripDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);
            await _uow.TripRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri, ToDto(await _uow.TripRepository.GetByIdAsync(newId)));
        }
    }

    /// <summary>
    /// Update the specified Trip.
    /// </summary>
    /// <param name="id">Id of the Trip.</param>
    /// <param name="value">New values of the Trip.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] TripDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        using (var trans = _uow.BeginTransaction())
        {
            var entity = await _uow.TripRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }

            entity.RouteId           = value.RouteId;
            entity.DepartureDateTime = value.DepartureDateTime;
            entity.ArrivalDateTime   = value.ArrivalDateTime;

            await trans.CommitTransactionAsync();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a Trip specified by the id.
    /// </summary>
    /// <param name="id">The id of the Trip.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entry = await _uow.TripRepository.GetByIdAsync(id);
            if (entry is not null)
            {
                _uow.TripRepository.Remove(entry);
            }

            await trans.CommitTransactionAsync();

            return NoContent();
        }
    }

    #endregion
}