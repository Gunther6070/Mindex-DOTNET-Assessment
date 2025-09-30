using CodeChallenge.Models;

namespace CodeChallenge.Services;

public interface IReportService
{
    ReportingStructure Create(string employeeId);

}