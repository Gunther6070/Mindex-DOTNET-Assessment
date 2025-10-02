using System;
using System.Collections.Generic;
using System.Data;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using CodeChallenge.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(CompensationService))]
public class CompensationServiceTest
{

    private Mock<IEmployeeService> _mockEmployeeService;
    private Mock<ICompensationRepository> _mockCompensationRepository;
    private ILogger<CompensationService> _logger;
    private CompensationService _compensationService;
    
    [TestInitialize]
    public void Setup()
    {
        _mockEmployeeService = new Mock<IEmployeeService>();
        _mockCompensationRepository = new Mock<ICompensationRepository>();
        _compensationService = new CompensationService(_logger, _mockCompensationRepository.Object, _mockEmployeeService.Object);
    }
    

    [TestMethod]
    public void CreateCompensation_Throws_NotFound()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "test",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        // Mock
        _mockEmployeeService.Setup(s => s.GetById(compensation.Employee)).Returns((Employee)null); // Mock return employee DNE
        
        // Execute
        var action = () => _compensationService.Create(compensation);
            
        // Assert
        Assert.ThrowsException<KeyNotFoundException>(action); // Employee doesn't exist
    }
    
    [TestMethod]
    public void CreateCompensation_Throws_Duplicate()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "test",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        // Mock
        _mockEmployeeService.Setup(s => s.GetById(compensation.Employee)).Returns(new Employee()); // Mock return employee
        _mockCompensationRepository.Setup(s => s.GetById(compensation.Employee)).Returns(compensation); // Mock return existing compensation
        
        // Execute
        var action = () => _compensationService.Create(compensation);
            
        // Assert
        Assert.ThrowsException<DuplicateNameException>(action); // Compensation already exists
    }
    
    [TestMethod]
    public void CreateCompensation()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "test",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        // Mock
        _mockEmployeeService.Setup(s => s.GetById(compensation.Employee)).Returns(new Employee());
        _mockCompensationRepository.Setup(s => s.GetById(compensation.Employee)).Returns((Compensation)null);
        _mockCompensationRepository.Setup(s => s.Add(compensation)).Returns(compensation);
        
        // Execute
        var newCompensation = _compensationService.Create(compensation);
            
        // Assert
        Assert.AreEqual(compensation.Employee, newCompensation.Employee);
        Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
    }

    
}