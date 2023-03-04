namespace SudInfo.Avalonia.Services;
public class ServerService
{
    public async Task<Result> AddServer(Server entity)
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
                Success = false,
                Message = ex.Message
            };
        }
    }
    public async Task<Result<Server>> GetServer(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Servers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            if (server == null)
                throw new Exception("Server not Found");
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
                Success = false,
                Message = ex.Message
            };
        }
    }
    public async Task<Result> RemoveServer(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (server == null)
                throw new Exception("Server not found");
            applicationDBContext.Remove(server);
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
    public async Task<Result> UpdateServer(Server entity)
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
                Success = false,
                Message = ex.Message
            };
        }
    }
}