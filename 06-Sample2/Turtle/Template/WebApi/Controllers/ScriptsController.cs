using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

/// <summary>
/// REST Controller for Game`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ScriptsController : ControllerBase
{
    private readonly IUnitOfWork                _uow;
    private readonly ILogger<ScriptsController> _logger;

    /// <summary>
    /// Constructor of ScriptsController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public ScriptsController(IUnitOfWork uow, ILogger<ScriptsController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// ScriptDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public record ScriptDto(
        int     Id,
        string  Name,
        string? Description
    );

    /// <summary>
    /// Convert dto to entity
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static Script ToEntity(ScriptDto dto)
    {
        return new Script()
        {
            Id          = dto.Id,
            Name        = dto.Name,
            Description = dto.Description,
        };
    }

    /// <summary>
    /// Convert entity to dto.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static ScriptDto? ToDto(Script? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ScriptDto(
            entity.Id,
            entity.Name,
            entity.Description
        );
    }

    /// <summary>
    /// Convert list ov entity to dto.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IList<ScriptDto>? ToDto(IList<Script>? list)
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
    /// Get all Scripts.
    /// </summary>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScriptDto>>> GetAsync(string? sort)
    {
        Func<IQueryable<Script>, IOrderedQueryable<Script>>? orderBy =
            sort switch
            {
                nameof(Script.Id)          => (query) => query.OrderBy(o => o.Id),
                nameof(Script.Description) => (query) => query.OrderBy(o => o.Description),
                _                          => null
            };

        var allEntities = await _uow.ScriptRepository.GetNoTrackingAsync(
            null,
            orderBy
        );

        return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Script.
    /// </summary>
    /// <param name="id">The id of the Script.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ScriptDto>> GetAsync(int id)
    {
        var entity = await _uow.ScriptRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    #endregion
}