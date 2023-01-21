using SudInfo.EFDataAccessLibrary.Models;

namespace SudInfo.Avalonia.Services;
public class MonitorsService : IMonitorsService
{
    #region Get Methods Realizations
    public async Task<TaskResult<List<Monitor>>> GetMonitors()
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var monitors = await applicationDBContext.Monitors.Include(x => x.Employee).ToListAsync();
            return new()
            {
                Success = true,
                Result = monitors
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
    public async Task<TaskResult<Monitor>> GetMonitorById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var monitor = await applicationDBContext.Monitors.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
            if (monitor == null)
                throw new Exception("Computer not Found");
            return new()
            {
                Success = true,
                Result = monitor
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

    public async Task<TaskResult> RemoveMonitorById(int id)
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
    public async Task<TaskResult> SaveMonitor(Monitor monitor)
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
    public async Task<TaskResult> AddMonitor(Monitor monitor)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            if (monitor.Employee != null)
            {
                monitor.Employee = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == monitor.Employee.Id);
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