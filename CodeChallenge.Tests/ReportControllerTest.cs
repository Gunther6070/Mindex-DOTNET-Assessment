using System.Net;
using System.Net.Http;
using CodeChallenge.Config;
using CodeChallenge.Controllers;
using CodeChallenge.Models;
using CodeChallenge.Tests.Integration.Helpers;
using CodeChallenge.Tests.Integration.Extensions;
using CodeChallenge.Tests.Integration.Helpers;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration;

[TestClass]
[TestSubject(typeof(ReportController))]
public class ReportControllerTest
{
    private static HttpClient _httpClient;
    private static TestServer _testServer;

    [ClassInitialize]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void InitializeClass(TestContext context)
    {
        _testServer = new TestServer();
        _httpClient = _testServer.NewClient();
    }
    
    [ClassCleanup]
    public static void CleanUpTest()
    {
        _httpClient.Dispose();
        _testServer.Dispose();
    }

    [TestMethod]
    public void CreateReport_Returns_200()
    {
        // Arrange
        var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309";
        var reportNum = 0;
        
        // Execute
        var getRequestTask = _httpClient.GetAsync($"api/report/{employeeId}");
        var response = getRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var report = response.DeserializeContent<ReportingStructure>();
        Assert.AreEqual(reportNum, report.NumberOfReports);

    }
    
    [TestMethod]
    public void CreateReport_Returns_404()
    {
        // Arrange
        var employeeId = "0";
        
        // Execute
        var getRequestTask = _httpClient.GetAsync($"api/report/{employeeId}");
        var response = getRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}