using System.Collections.Generic;
using System.Data;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services;

public class CompensationService : ICompensationService
{
    private readonly ICompensationRepository _compensationRepository;
    private readonly ILogger<CompensationService> _logger;
    private readonly IEmployeeService _employeeService;

    public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeService employeeService)
    {
        _compensationRepository = compensationRepository;
        _employeeService = employeeService;
        _logger = logger;
    }
        
    public Compensation Create(Compensation compensation)
    {
        if (compensation.Employee == null) throw new KeyNotFoundException();
        
        var employee = _employeeService.GetById(compensation.Employee);
        if (employee == null) throw new KeyNotFoundException();
        
        if (_compensationRepository.GetById(compensation.Employee) != null) throw new DuplicateNameException();

        compensation = _compensationRepository.Add(compensation);
        _compensationRepository.SaveAsync().Wait();

        return compensation;

    }

    public Compensation GetById(string employeeId)
    {
        if (string.IsNullOrEmpty(employeeId)) throw new KeyNotFoundException();
        
        var compensation = _compensationRepository.GetById(employeeId);
        if (compensation == null) throw new KeyNotFoundException();

        return compensation;
    }
}