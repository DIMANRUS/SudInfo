namespace SudInfo.Avalonia.Interfaces;
public interface IComputerService
{
    #region Get Methods
    Task<Result<List<Computer>>> GetComputers();
    Task<Result<Computer>> GetComputerById(int id);
    #endregion
    Task<Result> AddComputer(Computer computer);
    Task<Result> UpdateComputer(Computer computer);
    Task<Result> RemoveComputerById(int id);
}