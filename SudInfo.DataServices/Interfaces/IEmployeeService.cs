namespace SudInfo.DataServices.Interfaces;
public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployees();
}