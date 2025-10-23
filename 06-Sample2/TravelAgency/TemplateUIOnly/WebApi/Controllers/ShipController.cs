using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

/// <summary>
/// REST Controller for Ship`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShipController : ControllerBase
{
    private readonly IUnitOfWork             _uow;
    private readonly ILogger<ShipController> _logger;

    /// <summary>
    /// Constructor of ShipController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public ShipController(IUnitOfWork uow, ILogger<ShipController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// Ship Dto.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Owner"></param>
    /// <param name="PassengerCapacity"></param>
    /// <param name="CargoCapacity"></param>
    /// <param name="MaxSpeed"></param>
    public record ShipDto(
        int      Id,
        string   Name,
        string?  Owner,
        int?     PassengerCapacity,
        int?     CargoCapacity,
        decimal? MaxSpeed
    );

    Ship ToEntity(ShipDto dto)
    {
        return new Ship()
        {
            Id                = dto.Id,
            Name              = dto.Name,
            Owner             = dto.Owner,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity     = dto.CargoCapacity,
            MaxSpeed          = dto.MaxSpeed
        };
    }

    ShipDto? ToDto(Ship? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ShipDto(entity.Id, entity.Name, entity.Owner, entity.PassengerCapacity, entity.CargoCapacity, entity.MaxSpeed);
    }

    IList<ShipDto>? ToDto(IList<Ship>? list)
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
    /// Get all Ships.
    /// </summary>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipDto>>> GetAsync(string? sort)
    {
        Func<IQueryable<Ship>, IOrderedQueryable<Ship>>? orderBy =
            sort switch
            {
                nameof(Ship.Id)       => (query) => query.OrderBy(o => o.Id),
                nameof(Ship.Name)     => (query) => query.OrderBy(o => o.Name),
                nameof(Ship.Owner)    => (query) => query.OrderBy(o => o.Owner),
                nameof(Ship.MaxSpeed) => (query) => query.OrderBy(o => o.MaxSpeed),
                _                     => null
            };

        var allEntities = await _uow.ShipRepository.GetNoTrackingAsync(null, orderBy);

        return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Ship.
    /// </summary>
    /// <param name="id">The id of the Ship.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShipDto>> GetAsync(int id)
    {
        var entity = await _uow.ShipRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    /// <summary>
    /// Add a new Ship to the database.
    /// </summary>
    /// <param name="value">Values of the new Ship.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ShipDto>> AddAsync([FromBody] ShipDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);
            await _uow.ShipRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri, ToDto(await _uow.ShipRepository.GetByIdAsync(newId)));
        }
    }

    /// <summary>
    /// Update the specified Ship.
    /// </summary>
    /// <param name="id">Id of the Ship.</param>
    /// <param name="value">New values of the Ship.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ShipDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        using (var trans = _uow.BeginTransaction())
        {
            var entity = await _uow.ShipRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }

            entity.Name              = value.Name;
            entity.Owner             = value.Owner;
            entity.CargoCapacity     = value.CargoCapacity;
            entity.PassengerCapacity = value.PassengerCapacity;
            entity.MaxSpeed          = value.MaxSpeed;

            await trans.CommitTransactionAsync();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a Ship specified by the id.
    /// </summary>
    /// <param name="id">The id of the Ship.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entry = await _uow.ShipRepository.GetByIdAsync(id);
            if (entry is not null)
            {
                _uow.ShipRepository.Remove(entry);
            }

            await trans.CommitTransactionAsync();

            return NoContent();
        }
    }

    #endregion
}