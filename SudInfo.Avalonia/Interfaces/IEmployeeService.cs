namespace SudInfo.Avalonia.Interfaces;
public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployees();
}