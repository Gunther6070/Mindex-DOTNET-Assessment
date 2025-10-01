using System;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IReportService _reportService;
    
    public ReportController(ILogger<ReportController> logger, IReportService reportService)
    {
        _logger = logger;
        _reportService = reportService;
    }

    /// <summary>
    /// Get endpoint to get a reporting structure for an employee 
    /// </summary>
    /// <param name="id">
    /// ID of the desired employee
    /// </param>
    /// <returns>
    /// 200 if no error, 404 is employee doesn't exist
    /// </returns>
    [HttpGet("{id}", Name = "getReport")]
    public IActionResult GetReport(string id)
    {
        _logger.LogDebug("Received report get request for employee '{EmployeeId}'", id);
        try
        {
            var reportStructure = _reportService.Create(id); 
            return Ok(reportStructure);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: '{error}'", e);
            return NotFound();
        }



    }
    
}