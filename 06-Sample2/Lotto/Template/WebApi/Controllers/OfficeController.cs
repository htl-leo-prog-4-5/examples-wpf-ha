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
    private readonly ILogger<OfficeController> _logger;

    /// <summary>
    /// Constructor of OfficeController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public OfficeController(ILogger<OfficeController> logger)
    {
        _logger = logger;
    }
}