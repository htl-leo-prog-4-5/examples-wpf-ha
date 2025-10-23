using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

/// <summary>
/// REST Controller for Office`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IUnitOfWork               _uow;
    private readonly ILogger<OfficeController> _logger;

    /// <summary>
    /// Constructor of OfficeController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public OfficeController(IUnitOfWork uow, ILogger<OfficeController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    public record OfficeDto(int Id, string No, string Name, string Address);

    Office ToEntity(OfficeDto dto)
    {
        return new Office()
        {
            Id      = dto.Id,
            No      = dto.No,
            Name    = dto.Name,
            Address = dto.Address
        };
    }

    OfficeDto? ToDto(Office? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new OfficeDto(entity.Id, entity.No, entity.Name, entity.Address);
    }

    IList<OfficeDto>? ToDto(IList<Office>? list)
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
    /// Get all Offices.
    /// </summary>
    /// <param name="sort">Optional sort  by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfficeDto>>> GetAsync(string? sort)
    {
        Func<IQueryable<Office>, IOrderedQueryable<Office>>? orderBy =
            sort switch
            {
                nameof(Office.Id)      => (query) => query.OrderBy(o => o.Id),
                nameof(Office.No)      => (query) => query.OrderBy(o => o.No),
                nameof(Office.Name)    => (query) => query.OrderBy(o => o.Name),
                nameof(Office.Address) => (query) => query.OrderBy(o => o.Address),
                _                      => null
            };

        var allEntities = await _uow.OfficeRepository.GetNoTrackingAsync(null, orderBy);

        return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Office.
    /// </summary>
    /// <param name="id">The id of the Office.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OfficeDto>> GetAsync(int id)
    {
        var entity = await _uow.OfficeRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    /// <summary>
    /// Add a new Office to the database.
    /// </summary>
    /// <param name="value">Values of the new Office.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<OfficeDto>> AddAsync([FromBody] OfficeDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);
            await _uow.OfficeRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri, ToDto(await _uow.OfficeRepository.GetByIdAsync(newId)));
        }
    }

    /// <summary>
    /// Update the specified Office.
    /// </summary>
    /// <param name="id">Id of the Office.</param>
    /// <param name="value">New values of the Office.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] OfficeDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        using (var trans = _uow.BeginTransaction())
        {
            var entity = await _uow.OfficeRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }

            entity.No      = value.No;
            entity.Name    = value.Name;
            entity.Address = value.Address;
            await trans.CommitTransactionAsync();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a Office specified by the id.
    /// </summary>
    /// <param name="id">The id of the Office.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entry = await _uow.OfficeRepository.GetByIdAsync(id);
            if (entry is not null)
            {
                _uow.OfficeRepository.Remove(entry);
            }

            await trans.CommitTransactionAsync();

            return NoContent();
        }
    }

    #endregion
}