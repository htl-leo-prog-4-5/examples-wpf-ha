using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Core.Entities;

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
            //TODO: Convert Datastream to dto
            null
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

    //TODO: implement (used in overview, without datastreams)
    // public async Task<ActionResult<IEnumerable<ExaminationController.ExaminationDto>>> GetAsync(bool? withFindingsOnly, bool? addDataStreams, string? sort)

    //TODO: implement (used in findings, will load datastreams)
    // public async Task<ActionResult<ExaminationController.ExaminationDto>> GetAsync(int id)

    //TODO: implement to set medicalFinding, also set findingDate to DateTime.Now
    // public async Task<ActionResult> Update(int id, [FromBody] ExaminationDto value)
}