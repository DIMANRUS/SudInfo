namespace SudInfo.Avalonia.Interfaces;
public interface IMonitorService
{
    #region Get Methods
    Task<Result<List<Monitor>>> GetMonitors();
    Task<Result<Monitor>> GetMonitorById(int id); 
    #endregion
    Task<Result> RemoveMonitorById(int id);
    Task<Result> AddMonitor(Monitor computer);
    Task<Result> UpdateMonitor(Monitor computer);
}