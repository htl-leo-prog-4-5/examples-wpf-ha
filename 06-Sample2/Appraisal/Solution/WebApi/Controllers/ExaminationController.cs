using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.Entities;

using System.Linq.Expressions;

using Core.DataTransferObjects;

/// <summary>
/// REST Controller for Competition`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ExaminationController : ControllerBase
{
    private readonly IUnitOfWork                    _uow;
    private readonly ILogger<ExaminationController> _logger;

    /// <summary>
    /// Constructor of ExaminationController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public ExaminationController(IUnitOfWork uow, ILogger<ExaminationController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    /// <summary>
    /// ExaminationDto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ExaminationDate"></param>
    /// <param name="MedicalFindingsDate"></param>
    /// <param name="MedicalFindings"></param>
    /// <param name="SVNumber"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="DataStreams"></param>
    public record ExaminationDto(
        int                             Id,
        DateTime                        ExaminationDate,
        DateTime?                       MedicalFindingsDate,
        string?                         MedicalFindings,
        string                          SVNumber,
        string?                         FirstName,
        string?                         LastName,
        IList<ExaminationDataStreamDto> DataStreams);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="SeqNo"></param>
    /// <param name="Name"></param>
    /// <param name="Period"></param>
    /// <param name="Values"></param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    /// <param name="MinY"></param>
    /// <param name="MaxY"></param>
    public record ExaminationDataStreamDto(
        int           Id,
        int           SeqNo,
        string        Name,
        double        Period,
        IList<double> Values,
        double        Width,
        double        Height,
        double        MinY,
        double        MaxY
    );

    Examination ToEntity(ExaminationDto dto)
    {
        return new Examination()
        {
            Id = dto.Id,
        };
    }

    ExaminationDto? ToDto(Examination? entity, bool addDataStreams)
    {
        if (entity is null)
        {
            return null;
        }

        return new ExaminationDto(
            entity.Id,
            entity.ExaminationDate,
            entity.MedicalFindingsDate,
            entity.MedicalFindings,
            entity.Patient!.SVNumber,
            entity.Patient!.FirstName,
            entity.Patient!.LastName,
            addDataStreams
                ? entity.DataStreams!.Select((ds, idx) =>
                    {
                        var values = ds.MyValues;
                        var minY   = values.Min();
                        var maxY   = values.Max();

                        return new ExaminationDataStreamDto(
                            ds.Id,
                            idx,
                            ds.Name,
                            ds.Period,
                            values,
                            ds.Period * values.Count,
                            maxY - minY,
                            minY,
                            maxY
                        );
                    })
                    .ToList()
                : new List<ExaminationDataStreamDto>()
        );
    }

    IList<ExaminationDto>? ToDto(IList<Examination>? list, bool addDataStreams)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x, addDataStreams)!).ToList();
    }

    #endregion


    /// <summary>
    /// Get all Examinations.
    /// </summary>
    /// <param name="withFindingsOnly"></param>
    /// <param name="addDataStreams"></param>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExaminationController.ExaminationDto>>> GetAsync(bool? withFindingsOnly, bool? addDataStreams, string? sort)
    {
        Func<IQueryable<Examination>, IOrderedQueryable<Examination>>? orderBy =
            sort switch
            {
                nameof(Examination.Id)              => (query) => query.OrderBy(o => o.Id),
                nameof(Examination.ExaminationDate) => (query) => query.OrderBy(o => o.ExaminationDate),
                _                                   => null
            };

        Expression<Func<Examination, bool>>? filter = null;

        if (withFindingsOnly ?? true)
        {
            filter = examination => examination.MedicalFindings == null;
        }

        var allEntities = await _uow.ExaminationRepository.GetNoTrackingAsync(
            filter,
            orderBy,
            nameof(Examination.Patient), nameof(Examination.DataStreams)
        );

        return await this.NotFoundOrOk(ToDto(allEntities, addDataStreams ?? false));
    }

    /// <summary>
    /// Get a specified Examination.
    /// </summary>
    /// <param name="id">The id of the Examination.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExaminationController.ExaminationDto>> GetAsync(int id)
    {
        var entity = await _uow.ExaminationRepository.GetByIdAsync(id, nameof(Examination.Patient), nameof(Examination.DataStreams));
        ;
        return await this.NotFoundOrOk(ToDto(entity, true));
    }

    /// <summary>
    /// Update the specified Examination.
    /// </summary>
    /// <param name="id">Id of the Examination.</param>
    /// <param name="value">Only the MedicalFindings will be updated.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ExaminationDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        var entity = await _uow.ExaminationRepository.GetByIdAsync(id, nameof(Examination.Patient), nameof(Examination.DataStreams));
        if (entity is null)
        {
            return NotFound();
        }

        entity.MedicalFindings     = value.MedicalFindings;
        entity.MedicalFindingsDate = DateTime.Now;

        await _uow.SaveChangesAsync();

        return Ok();
    }
}