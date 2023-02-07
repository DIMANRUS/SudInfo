namespace SudInfo.Avalonia.Services;
public class MonitorService : IMonitorService
{
    #region Get Methods Realizations
    public async Task<Result<List<Monitor>>> GetMonitors()
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var monitors = await applicationDBContext.Monitors.Include(x => x.Computer).ThenInclude(x => x.User).ToListAsync();
            return new()
            {
                Success = true,
                Object = monitors
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
    public async Task<Result<Monitor>> GetMonitorById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
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

    public async Task<Result> RemoveMonitorById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
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
    public async Task<Result> UpdateMonitor(Monitor monitor)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            applicationDBContext.Monitors.Update(monitor);
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
    public async Task<Result> AddMonitor(Monitor monitor)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
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