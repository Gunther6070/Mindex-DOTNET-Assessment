using System;
using System.Collections.Generic;

namespace CodeChallenge.Models;

public class ReportingStructure
{
    public Employee Employee { get; set; }
    public int NumberOfReports { get => CountReports(Employee.DirectReports);}
    
    /// <summary>
    /// Calculates the number of employees that report to a given employee
    /// </summary>
    /// <param name="reporters">
    /// A list of employees
    /// </param>
    /// <returns>
    /// The number of employees in a reporting structure
    /// </returns>
    private static int CountReports(List<Employee> reporters)
    {
        var sum = 0;
        if (reporters == null || reporters.Count == 0)
        {
            return 0;
        }
        
        foreach (var employee in reporters)
        {
            sum += CountReports(employee.DirectReports) + 1;
        }

        return sum;
    }
    
}