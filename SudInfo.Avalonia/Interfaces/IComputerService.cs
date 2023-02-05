namespace SudInfo.Avalonia.Interfaces;
public interface IComputerService
{
    #region Get Methods
    Task<Result<List<Computer>>> GetComputers();
    Task<Result<Computer>> GetComputerById(int id);
    Task<Result<List<Computer>>> GetComputerNames();
    #endregion
    Task<Result> AddComputer(Computer computer);
    Task<Result> UpdateComputer(Computer computer);
    Task<Result> RemoveComputerById(int id);
}