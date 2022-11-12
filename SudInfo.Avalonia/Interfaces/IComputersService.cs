using SudInfo.Avalonia.Models;

namespace SudInfo.Avalonia.Interfaces;
public interface IComputersService
{
    #region Get Computer/s
    Task<TaskResult<List<Computer>>> GetComputers();
    Task<TaskResult<Computer>> GetComputerById(int id);
    #endregion
    Task<TaskResult> AddComputer(Computer computer);
    Task<TaskResult> SaveComputer(Computer computer);
    Task<TaskResult> RemoveComputerById(int id);
}