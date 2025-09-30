using CodeChallenge.Models;

namespace CodeChallenge.Services;

public interface IReportService
{
    ReportingStructure GetByEmployee(Employee employee);
    ReportingStructure Create(ReportingStructure report);
    ReportingStructure Replace(Employee oldEmployee, Employee newEmployee);

}