using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

using System.Linq.Expressions;

/// <summary>
/// REST Controller for Competition`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CompetitionsController : ControllerBase
{
    private readonly IUnitOfWork                     _uow;
    private readonly ILogger<CompetitionsController> _logger;

    /// <summary>
    /// Constructor of CompetitionsController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public CompetitionsController(IUnitOfWork uow, ILogger<CompetitionsController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// CompetitionDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Description"></param>
    /// <param name="Active"></param>
    /// <param name="Scripts"></param>
    public record CompetitionDto(
        int                                 Id,
        string                              Description,
        bool                                Active,
        IList<ScriptsController.ScriptDto>? Scripts);

    Competition ToEntity(CompetitionDto dto)
    {
        return new Competition()
        {
            Id          = dto.Id,
            Description = dto.Description,
            Active      = dto.Active,
            Scripts     = dto.Scripts?.Select(ScriptsController.ToEntity).ToList()
        };
    }

    CompetitionDto? ToDto(Competition? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new CompetitionDto(
            entity.Id,
            entity.Description,
            entity.Active,
            ScriptsController.ToDto(entity.Scripts)
        );
    }

    IList<CompetitionDto>? ToDto(IList<Competition>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    #endregion

    /// <summary>
    /// Get all Competitions.
    /// </summary>
    /// <param name="activeOnly"></param>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetitionDto>>> GetAsync(bool? activeOnly, string? sort)
    {
        Func<IQueryable<Competition>, IOrderedQueryable<Competition>>? orderBy =
            sort switch
            {
                nameof(Competition.Id)          => (query) => query.OrderBy(o => o.Id),
                nameof(Competition.Description) => (query) => query.OrderBy(o => o.Description),
                _                               => null
            };

        Expression<Func<Competition, bool>>? filter = null;

        if (activeOnly ?? true)
        {
            filter = competition => competition.Active == true;
        }

        var allEntities = await _uow.CompetitionRepository.GetNoTrackingAsync(
            filter,
            orderBy,
            nameof(Competition.Scripts)
        );

        return await this.NotFoundOrOk(ToDto(allEntities));
    }

    /// <summary>
    /// Get a specified Competition.
    /// </summary>
    /// <param name="id">The id of the Competition.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CompetitionDto>> GetAsync(int id)
    {
        var entity = await _uow.CompetitionRepository.GetByIdAsync(id,
            nameof(Competition.Scripts)
        );
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    /// <summary>
    /// Add a new Competition to the database.
    /// </summary>
    /// <param name="value">Values of the new Competition.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CompetitionDto>> AddAsync([FromBody] CompetitionDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);

            //TODO: use tracking objects (e.g. for Scripts)

            await _uow.CompetitionRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri, ToDto(await _uow.CompetitionRepository.GetByIdAsync(newId,
                nameof(Competition.Scripts)
            )));
        }
    }
}