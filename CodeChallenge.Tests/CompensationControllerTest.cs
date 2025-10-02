using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Controllers;
using CodeChallenge.Models;
using CodeChallenge.Tests.Integration.Extensions;
using CodeChallenge.Tests.Integration.Helpers;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(CompensationController))]
public class CompensationControllerTest
{

    private HttpClient _httpClient;
    private TestServer _testServer;

    [TestInitialize]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public void InitializeClass()
    {
        _testServer = new TestServer();
        _httpClient = _testServer.NewClient();
    }
    
    [TestCleanup]
    public void CleanUpTest()
    {
        _httpClient.Dispose();
        _testServer.Dispose();
    }

    [TestMethod]
    public void GetCompensation_Returns_404()
    {
        // Arrange
        var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
        
        // Execute
        var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
        var response = getRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode); // Returns not found as a compensation for the employee hasn't been created yet

    }
    
    [TestMethod]
    public void CreateCompensation_Returns_404()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "test-test",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        var requestContent = new JsonSerialization().ToJson(compensation);
        
        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    
    [TestMethod]
    public void CreateCompensation_Returns_201()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "b7839309-3348-463b-a7e3-5de1c168beb3",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        var requestContent = new JsonSerialization().ToJson(compensation);
        
        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        
        var newCompensation = response.DeserializeContent<Compensation>();
        Assert.AreEqual(compensation.Employee, newCompensation.Employee);
        Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
    }
    
    [TestMethod]
    public void CreateCompensation_Returns_409()
    {
        // Arrange
        var compensation = new Compensation()
        {
            Employee = "b7839309-3348-463b-a7e3-5de1c168beb3",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        var requestContent = new JsonSerialization().ToJson(compensation);
        
        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var postRequestTask2 = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode); // Returns conflict as a compensation already exists
        
        
    }
    
    [TestMethod]
    public void GetCompensation_Returns_200()
    {
        // Arrange
        var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
        
        var compensation = new Compensation()
        {
            Employee = "b7839309-3348-463b-a7e3-5de1c168beb3",
            Salary = 70000,
            EffectiveDate = Convert.ToDateTime("10-7-2027")
        };

        var requestContent = new JsonSerialization().ToJson(compensation);
        
        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;
        
        var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
        response = getRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    }
    
}