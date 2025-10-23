using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.DataTransferObjects;
using Core.Entities;

/// <summary>
/// REST Controller for Game`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class StationController : ControllerBase
{
    private readonly IUnitOfWork                _uow;
    private readonly ILogger<StationController> _logger;

    /// <summary>
    /// Constructor of GameController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public StationController(IUnitOfWork uow, ILogger<StationController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// StationDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Code"></param>
    /// <param name="Type"></param>
    /// <param name="StateCode"></param>
    /// <param name="IsRegional"></param>
    /// <param name="IsExpress"></param>
    /// <param name="IsIntercity"></param>
    /// <param name="Remark"></param>
    /// <param name="City"></param>
    /// <param name="Infrastructures"></param>
    /// <param name="RailwayCompanies"></param>
    /// <param name="Lines"></param>
    public record StationDto(
        int           Id,
        string        Name,
        string?       Code,
        string?       Type,
        string        StateCode,
        bool          IsRegional,
        bool          IsExpress,
        bool          IsIntercity,
        string?       Remark,
        string?       City,
        IList<string> Infrastructures,
        IList<string> RailwayCompanies,
        IList<string> Lines
    );

    Station ToEntity(StationDto dto)
    {
        return new Station()
        {
            Id          = dto.Id,
            Name        = dto.Name,
            Code        = dto.Code,
            Type        = dto.Type,
            StateCode   = dto.StateCode,
            IsRegional  = dto.IsRegional,
            IsExpress   = dto.IsExpress,
            IsIntercity = dto.IsIntercity,
            Remark      = dto.Remark,
        };
    }

    StationDto? ToDto(Station? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new StationDto(
            entity.Id,
            entity.Name,
            entity.Code,
            entity.Type,
            entity.StateCode,
            entity.IsRegional,
            entity.IsExpress,
            entity.IsIntercity,
            entity.Remark,
            entity.City?.Name ?? string.Empty,
            entity.Infrastructures?.Select(x => x.Name).ToList() ?? new List<string>(),
            entity.RailwayCompanies?.Select(x => x.Name).ToList() ?? new List<string>(),
            entity.Lines?.Select(x => x.Name).ToList() ?? new List<string>()
        );
    }

    IList<StationDto>? ToDto(IList<Station>? list)
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
    /// Get all Stations.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StationDto>>> GetAsync()
    {
        throw new NotImplementedException();
        //return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Station.
    /// </summary>
    /// <param name="id">The id of the Station.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StationDto>> GetAsync(int id)
    {
        throw new NotImplementedException();
/*
        var entity = await _uow.Stationrepository.GetByIdAsync(id,
            nameOf(Station.City),
            nameof(Station.RailwayCompanies),
            nameof(Station.Infrastructures),
            nameof(Station.Lines)
        );
        ;
        return await this.NotFoundOrOk(ToDto(entity));
*/
    }

    /// <summary>
    /// Add a new Station to the database.
    /// </summary>
    /// <param name="value">Values of the new Station.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<StationDto>> AddAsync([FromBody] StationDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);
            await _uow.StationRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri,
                ToDto(await _uow.StationRepository.GetByIdAsync(newId,
                    nameof(Station.City),
                    nameof(Station.RailwayCompanies),
                    nameof(Station.Infrastructures),
                    nameof(Station.Lines)
                )));
        }
    }

    /// <summary>
    /// Update the specified Station.
    /// </summary>
    /// <param name="id">Id of the Station.</param>
    /// <param name="value">New values of the Station.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] StationDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        using (var trans = _uow.BeginTransaction())
        {
            var entity = await _uow.StationRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }
/*
            entity.Name        = value.Name;
            entity.Code        = value.Code;
            entity.Type        = value.Type;
            entity.StateCode   = value.StateCode;
            entity.IsRegional  = value.IsRegional;
            entity.IsExpress   = value.IsExpress;
            entity.IsIntercity = value.IsIntercity;
            entity.Remark      = value.Remark;
*/
            throw new NotImplementedException();

            await trans.CommitTransactionAsync();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a Station specified by the id.
    /// </summary>
    /// <param name="id">The id of the Station.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entry = await _uow.StationRepository.GetByIdAsync(id);
            if (entry is not null)
            {
                _uow.StationRepository.Remove(entry);
            }

            await trans.CommitTransactionAsync();

            return NoContent();
        }
    }

    #endregion
}