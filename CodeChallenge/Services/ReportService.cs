using System;
using System.Collections.Generic;
using CodeChallenge.Models;

namespace CodeChallenge.Services;

public class ReportService : IReportService
{

    private readonly IEmployeeService _employeeService;

    public ReportService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    /// <summary>
    /// Returns a new reporting structure for the given employee
    /// </summary>
    /// <param name="employeeId">
    /// The ID of an employee
    /// </param>
    /// <returns>
    /// A reporting structure for the given employee
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Throws error if an employee with the given ID does not exist
    /// </exception>
    public ReportingStructure Create(string employeeId)
    {
        var employee = _employeeService.GetById(employeeId);
        
        if (employee == null)
        {
            throw new KeyNotFoundException(employeeId);
        }
        Console.WriteLine(employee.DirectReports);

        return new ReportingStructure() { Employee = employee };

    }

}