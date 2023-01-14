namespace SudInfo.Avalonia.Interfaces;
public interface IComputersService
{
    #region Get Methods
    Task<TaskResult<List<Computer>>> GetComputers();
    Task<TaskResult<Computer>> GetComputerById(int id);
    #endregion
    Task<TaskResult> AddComputer(Computer computer);
    Task<TaskResult> SaveComputer(Computer computer);
    Task<TaskResult> RemoveComputerById(int id);
}