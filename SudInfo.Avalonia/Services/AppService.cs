namespace SudInfo.Avalonia.Services;
public class AppService
{
    public async Task<Result<IReadOnlyList<AppEntity>>> GetApps()
    {
        try
        {
            using SudInfoDbContext context = new();
            var result = await context.Apps.AsNoTracking().ToListAsync();
            return new()
            {
                Object = result,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
    public async Task<Result> RemoveApp(int id)
    {
        try
        {
            using SudInfoDbContext context = new();
            var entity = await context.Apps.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Password not found");
            context.Apps.Remove(entity);
            await context.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
    public async Task<Result<AppEntity>> GetApp(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Apps
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("App not Found");
            return new()
            {
                Success = true,
                Object = server
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
    public async Task<Result> UpdatePassword(PasswordEntity entity)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            applicationDBContext.Update(entity);
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
                Message = ex.Message
            };
        }
    }
    public async Task<Result> AddPassword(PasswordEntity entity)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            await applicationDBContext.AddAsync(entity);
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
                Message = ex.Message
            };
        }
    }
}