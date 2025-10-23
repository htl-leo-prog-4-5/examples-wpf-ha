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
public class GameController : ControllerBase
{
    private readonly IUnitOfWork             _uow;
    private readonly ILogger<GameController> _logger;

    /// <summary>
    /// Constructor of GameController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public GameController(IUnitOfWork uow, ILogger<GameController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// GameDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="DateFrom"></param>
    /// <param name="DateTo"></param>
    /// <param name="ExpectedDrawDate"></param>
    /// <param name="MaxNo"></param>
    public record GameDto(
        int      Id,
        DateOnly DateFrom,
        DateOnly DateTo,
        DateOnly ExpectedDrawDate,
        int      MaxNo);

    Game ToEntity(GameDto dto)
    {
        return new Game()
        {
            Id               = dto.Id,
            DateFrom         = dto.DateFrom,
            DateTo           = dto.DateTo,
            ExpectedDrawDate = dto.ExpectedDrawDate,
            MaxNo            = dto.MaxNo,
        };
    }

    GameDto? ToDto(Game? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new GameDto(entity.Id, entity.DateFrom, entity.DateTo, entity.ExpectedDrawDate, entity.MaxNo);
    }

    IList<GameDto>? ToDto(IList<Game>? list)
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
    /// Get all Games.
    /// </summary>
    /// <param name="state">e.g. open, to get the current open games.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAsync(string? state)
    {
        IList<Game> games;

        if ((state ?? string.Empty) == "open")
        {
            games = await _uow.GameRepository.GetCurrentOpenGamesAsync(DateOnly.FromDateTime(DateTime.Today));
        }
        else
        {
            games = await _uow.GameRepository.GetNoTrackingAsync();
        }

        return await this.NotFoundOrOk(ToDto(games));
    }

    /// <summary>
    /// Get a specified Game.
    /// </summary>
    /// <param name="id">The id of the Game.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameDto>> GetAsync(int id)
    {
        var entity = await _uow.GameRepository.GetByIdAsync(id);
        ;
        return await this.NotFoundOrOk(ToDto(entity));
    }

    #endregion
}