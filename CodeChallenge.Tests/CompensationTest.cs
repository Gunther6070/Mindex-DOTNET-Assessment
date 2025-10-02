using System;
using CodeChallenge.Models;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(Compensation))]
public class CompensationTest
{

    [TestMethod]
    public void CreateCompensation()
    {
        // Arrange
        var employee = "b7839309-3348-463b-a7e3-5de1c168beb3";
        var salary = 70000;
        var effectiveDate = Convert.ToDateTime("10-7-2027");
        
        
        var compensation = new Compensation()
        {
            Employee = "b7839309-3348-463b-a7e3-5de1c168beb3",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };
        
        
        // Assert
        Assert.AreEqual(employee, compensation.Employee);
        Assert.AreEqual(salary, compensation.Salary);
        Assert.AreEqual(effectiveDate, compensation.EffectiveDate);
    }
}