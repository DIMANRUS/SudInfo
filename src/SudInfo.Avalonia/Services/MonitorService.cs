namespace SudInfo.Avalonia.Services;

public class MonitorService : BaseService
{
    #region Get Methods Realizations
    public static async Task<IReadOnlyCollection<Monitor>> GetMonitors()
    {
        using SudInfoDbContext applicationDBContext = new();
        var monitors = await applicationDBContext.Monitors.Include(x => x.Computer).ThenInclude(x => x.User).ToListAsync();
        return monitors;
    }
    public static async Task<Result<Monitor>> GetMonitorById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var monitor = await applicationDBContext.Monitors.Include(x => x.Computer).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (monitor == null)
                throw new Exception("Computer not Found");
            return new()
            {
                Success = true,
                Object = monitor
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    #endregion

    public static async Task<Result> RemoveMonitorById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var monitor = await applicationDBContext.Monitors.FirstOrDefaultAsync(x => x.Id == id);
            if (monitor == null)
                throw new Exception("Monitor not found");
            applicationDBContext.Monitors.Remove(monitor);
            await applicationDBContext.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    public static async Task<Result> AddMonitor(Monitor monitor)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            if (monitor.Computer != null)
            {
                monitor.Computer = await applicationDBContext.Computers.SingleOrDefaultAsync(x => x.Id == monitor.Computer.Id);
            }
            await applicationDBContext.Monitors.AddAsync(monitor);
            await applicationDBContext.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}