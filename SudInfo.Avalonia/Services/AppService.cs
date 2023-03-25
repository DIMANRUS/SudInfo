namespace SudInfo.Avalonia.Services;
public class AppService
{
    public async Task<Result<IReadOnlyList<AppEntity>>> GetApps()
    {
        try
        {
            await using SudInfoDbContext context = new();
            var result = await context.Apps
                .AsNoTracking()
                .Include(x => x.Computers)
                .ThenInclude(x=>x.User)
                .ToListAsync();
            return new Result<IReadOnlyList<AppEntity>>
            {
                Object = result,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new Result<IReadOnlyList<AppEntity>>
            {
                Message = ex.Message
            };
        }
    }
    public async Task<Result> RemoveApp(int id)
    {
        try
        {
            await using SudInfoDbContext context = new();
            var entity = await context.Apps.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Password not found");
            entity.Computers = null;
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
    public async Task<Result<AppEntity>> GetApp(int id)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var server = await applicationDbContext.Apps
                .AsNoTracking()
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
    public async Task<Result> UpdateApp(AppEntity entity)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var computers = entity.Computers;
            entity.Computers = new List<Computer>();
            foreach (var computer in computers)
            {
                entity.Computers.Add(await applicationDbContext.Computers.FirstAsync(x=>x.Id == computer.Id));
            }
            applicationDbContext.Update(entity);
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
    public async Task<Result> AddApp(AppEntity entity)
    {
        try
        {
            await using SudInfoDbContext applicationDbContext = new();
            var computers = entity.Computers;
            entity.Computers = new List<Computer>();
            foreach (var computer in computers)
            {
                entity.Computers.Add(await applicationDbContext.Computers.FirstAsync(x=>x.Id == computer.Id));
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