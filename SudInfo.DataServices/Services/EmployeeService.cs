namespace SudInfo.DataServices;
public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDBContext _context;
    public EmployeeService(ApplicationDBContext applicationDBContext)
    {
        _context = applicationDBContext;
    }
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }
}