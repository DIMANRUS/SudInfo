namespace SudInfo.Avalonia.Interfaces;
public interface IPeripheryService
{
    #region Get Methods
    Task<Result<List<Periphery>>> GetPeripheryList();
    Task<Result<Periphery>> GetPeripheryById(int id);
    #endregion
    Task<Result> UpdatePeriphery(Periphery periphery);
    Task<Result> AddPeriphery(Periphery periphery);
    Task<Result> RemovePeripheryById(int id);
}