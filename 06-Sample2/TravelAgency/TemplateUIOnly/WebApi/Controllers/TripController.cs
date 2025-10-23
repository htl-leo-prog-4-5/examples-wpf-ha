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


    #endregion
}