using CodeChallenge.Models;
using CodeChallenge.Services;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(ReportService))]
public class ReportServiceTest
{

    private Mock<IEmployeeService> _mockEmployeeService;
    private ReportService _reportService;
    
    [TestInitialize]
    public void Setup()
    {
        _mockEmployeeService = new Mock<IEmployeeService>();
        _reportService = new ReportService(_mockEmployeeService.Object);
    }
    

    [TestMethod]
    public void CreateReport()
    {
        // Arrange
        var employee = new Employee
        {
            FirstName = "Foo",
            LastName = "Bar",
            Department = "Test",
            EmployeeId = "0",
            Position = "foobar"

        };
        var report = new ReportingStructure()
        {
            Employee = employee
        };

        // Mock
        _mockEmployeeService.Setup(s => s.GetById(employee.EmployeeId)).Returns(employee);
        
        // Execute
        var actualReport = _reportService.Create(employee.EmployeeId);
            
        // Assert
        Assert.AreEqual(report.Employee, actualReport.Employee);
        Assert.AreEqual(report.NumberOfReports, actualReport.NumberOfReports);
    }
}