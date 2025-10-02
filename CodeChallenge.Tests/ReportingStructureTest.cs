using System;
using System.Collections.Generic;
using CodeChallenge.Models;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(ReportingStructure))]
public class ReportingStructureTest
{
    
    [TestMethod]
    public void CreateReportStructure()
    {
        // Arrange
        var reportSize = 2;
        var firstName = "Foo";
        
        var employee = new Employee();
        var employee2 = new Employee();
        var employee3 = new Employee
        {
            FirstName = "Foo",
            LastName = "Bar",
            Department = "Test",
            EmployeeId = "0",
            Position = "foobar",
            DirectReports = new List<Employee> { employee, employee2 }

        };
        
        // Execute
        var reports = new ReportingStructure()
        {
            Employee = employee3
        };
        
        // Assert
        Assert.AreEqual(reportSize, reports.NumberOfReports);
        Assert.AreEqual(firstName, employee3.FirstName);
        Assert.IsNull(employee.DirectReports);
    }
}