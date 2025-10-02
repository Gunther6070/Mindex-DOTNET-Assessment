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
        
    /// <summary>
    /// Creates a persistent Compensation object
    /// </summary>
    /// <param name="compensation">
    /// The passed compensation to create
    /// </param>
    /// <returns>
    /// Returns the created compensation
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Throws a KeyNotFound exception if the given employee doesn't exist
    /// </exception>
    /// <exception cref="DuplicateNameException">
    /// Throws a Duplicate exception if a compensation for an employee already exists
    /// </exception>
    public Compensation Create(Compensation compensation)
    {
        if (string.IsNullOrEmpty(compensation.Employee)) throw new KeyNotFoundException();
        
        var employee = _employeeService.GetById(compensation.Employee);
        if (employee == null) throw new KeyNotFoundException();
        
        if (_compensationRepository.GetById(compensation.Employee) != null) throw new DuplicateNameException();

        compensation = _compensationRepository.Add(compensation);
        _compensationRepository.SaveAsync().Wait();

        return compensation;

    }

    /// <summary>
    /// Returns a Compensation for the given employeeId
    /// </summary>
    /// <param name="employeeId">
    /// The ID of an employee
    /// </param>
    /// <returns>
    /// A compensation for an employee
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Throws KeyNotFound exception if the employee or compensation doesn't exist
    /// </exception>
    public Compensation GetById(string employeeId)
    {
        if (string.IsNullOrEmpty(employeeId)) throw new KeyNotFoundException();
        
        var compensation = _compensationRepository.GetById(employeeId);
        if (compensation == null) throw new KeyNotFoundException();

        return compensation;
    }
}