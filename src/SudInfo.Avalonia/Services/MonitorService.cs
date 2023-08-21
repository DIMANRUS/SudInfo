namespace SudInfo.Avalonia.Services;

public class MonitorService : BaseService<Monitor>
{
    #region Ctors

    public MonitorService(SudInfoDatabaseContext context) : base(context) { }

    #endregion

    #region Get Methods

    public async Task<IReadOnlyCollection<Monitor>> Get()
    {
        var monitors = await context.Monitors.AsNoTracking()
                                             .Include(x => x.Computer)
                                             .ThenInclude(x => x.User)
                                             .ToListAsync();
        return monitors;
    }

    public async Task<Result<Monitor>> Get(int id)
    {
        try
        {
            var monitor = await context.Monitors.AsNoTracking()
                                                .Include(x => x.Computer)
                                                .ThenInclude(x => x.User)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return monitor == null ? throw new Exception("Computer not Found") : new(monitor, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var monitor = await context.Monitors.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Monitor not found");
            context.Monitors.Remove(monitor);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }

    public override async Task<Result> Add(Monitor monitor)
    {
        try
        {
            if (monitor.Computer != null)
            {
                monitor.Computer = await context.Computers.SingleOrDefaultAsync(x => x.Id == monitor.Computer.Id);
            }
            await context.Monitors.AddAsync(monitor);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}