using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers;

[ApiController]
[Route("api/compensation")]
public class CompensationController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICompensationService _compensationService;
    
    public CompensationController(ILogger<ReportController> logger, ICompensationService compensationService)
    {
        _logger = logger;
        _compensationService = compensationService;
    }
    
    /// <summary>
    /// Post endpoint to create a new compensation 
    /// </summary>
    /// <param name="compensation">
    /// Compensation to create
    /// </param>
    /// <returns>
    /// 200 if no error, 409 is compensation already exists
    /// </returns>
    [HttpPost]
    public IActionResult CreateCompensation([FromBody] Compensation compensation)
    {
        _logger.LogDebug("Received compensation create request for compensation '{compensation}'", compensation);
        try
        {
            var newCompensation = _compensationService.Create(compensation); 
            return CreatedAtRoute("getCompensation", new { id = newCompensation.Employee.EmployeeId }, compensation);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: '{error}'", e);
            return Conflict();
        }

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
    [HttpGet("{id}", Name = "getCompensation")]
    public IActionResult GetCompensation(string id)
    {
        _logger.LogDebug("Received compensation get request for employee '{EmployeeId}'", id);
        try
        {
            var reportStructure = _compensationService.GetById(id); 
            return Ok(reportStructure);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: '{error}'", e);
            return NotFound();
        }



    }
}