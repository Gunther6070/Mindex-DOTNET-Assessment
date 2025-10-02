using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace CodeChallenge.Repositories;

public class CompensationRepository : ICompensationRepository
{
    private readonly CompensationContext _compensationContext;
    private readonly ILogger<IEmployeeRepository> _logger;

    public CompensationRepository(ILogger<IEmployeeRepository> logger, CompensationContext compensationContext)
    {
        _compensationContext = compensationContext;
        _logger = logger;
    }
    
    public Compensation Add(Compensation compensation)
    {
        compensation.EffectiveDate = DateAndTime.Now;
        _compensationContext.Compensations.Add(compensation);
        return compensation;
    }
    
    public Compensation GetById(string id)
    {
        return _compensationContext.Compensations.SingleOrDefault(e => e.Employee == id);
    }
    
    public Task SaveAsync()
    {
        return _compensationContext.SaveChangesAsync();
    }

    
}