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
    
    /// <summary>
    /// Adds a compensation to persistence
    /// </summary>
    /// <param name="compensation">
    /// Compensation to add
    /// </param>
    /// <returns>
    /// The added compensation
    /// </returns>
    public Compensation Add(Compensation compensation)
    {
        _compensationContext.Compensations.Add(compensation);
        return compensation;
    }
    
    /// <summary>
    /// Get a compensation from persistence
    /// </summary>
    /// <param name="id">
    /// The id of an employee
    /// </param>
    /// <returns>
    /// A compensation for the provided employee
    /// </returns>
    public Compensation GetById(string id)
    {
        return _compensationContext.Compensations.SingleOrDefault(e => e.Employee == id);
    }
    
    /// <summary>
    /// Save changes to the in memory database
    /// </summary>
    /// <returns>
    /// A task resulting from saving
    /// </returns>
    public Task SaveAsync()
    {
        return _compensationContext.SaveChangesAsync();
    }

    
}