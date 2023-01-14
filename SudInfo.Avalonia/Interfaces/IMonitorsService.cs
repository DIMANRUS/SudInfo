namespace SudInfo.Avalonia.Interfaces;
public interface IMonitorsService
{
    #region Get Methods
    Task<TaskResult<List<Monitor>>> GetMonitors();
    Task<TaskResult<Monitor>> GetMonitorById(int id); 
    #endregion
    Task<TaskResult> RemoveMonitorById(int id);
    Task<TaskResult> AddMonitor(Monitor computer);
    Task<TaskResult> SaveMonitor(Monitor computer);
}