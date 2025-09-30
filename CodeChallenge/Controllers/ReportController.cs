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
    
    
    
}