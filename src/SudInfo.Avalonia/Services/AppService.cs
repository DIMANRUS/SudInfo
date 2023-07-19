namespace SudInfo.Avalonia.Services;

public class AppService
{
    public static async Task<IReadOnlyCollection<AppEntity>> GetApps()
    {
        await using SudInfoDbContext context = new();
        var apps = await context.Apps
            .AsNoTracking()
            .Include(x => x.Computers)
            .ThenInclude(x => x.User)
            .ToListAsync();
        return apps;
    }
    public static async Task<Result> RemoveApp(int id)
    {
        try
        {
            await using SudInfoDbContext context = new();
            var entity = await context.Apps.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Password not found");
            entity.Computers.Clear();
            context.Apps.Remove(entity);
            await context.SaveChangesAsync();
            return new Result
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new Result
            {
                Message = ex.Message
            };
        }
    }
    public static async Task<Result<AppEntity>> GetApp(int id)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var server = await applicationDbContext.Apps
                .AsNoTracking()
                .Include(x => x.Computers)
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("App not Found");
            return new Result<AppEntity>
            {
                Success = true,
                Object = server
            };
        }
        catch (Exception ex)
        {
            return new Result<AppEntity>
            {
                Message = ex.Message
            };
        }
    }
    public static async Task<Result> UpdateApp(AppEntity entity)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var app = await applicationDbContext.Apps.Include(x => x.Computers).SingleAsync(x => x.Id == entity.Id);
            app.Name = entity.Name;
            app.Version = entity.Version;
            if (entity.Computers.Count == 0)
            {
                app.Computers = null;
            }
            else
            {
                app.Computers = new List<Computer>();
                foreach (var computer in entity.Computers)
                {
                    app.Computers.Add(await applicationDbContext.Computers.FirstAsync(x => x.Id == computer.Id));
                }
            }
            applicationDbContext.Update(app);
            await applicationDbContext.SaveChangesAsync();
            return new Result
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new Result
            {
                Message = ex.Message
            };
        }
    }
    public static async Task<Result> AddApp(AppEntity entity)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var computers = entity.Computers;
            entity.Computers = new List<Computer>();
            foreach (var computer in computers)
            {
                entity.Computers.Add(await applicationDbContext.Computers.FirstAsync(x => x.Id == computer.Id));
            }
            await applicationDbContext.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();
            return new Result
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new Result
            {
                Message = ex.Message
            };
        }
    }
}