namespace SudInfo.Avalonia.Services;
public class ServerRackService
{
    public async Task<Result> AddServerRack(ServerRack entity)
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
    public async Task<Result<IReadOnlyList<ServerRack>>> GetServerRacksWithServers()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var serverRacks = await applicationDBContext.ServerRacks
                .AsNoTracking()
                .Include(x=>x.Servers)
                .ToListAsync();
            return new()
            {
                Success = true,
                Object = serverRacks
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
    public async Task<Result<ServerRack>> GetServerRack(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var serverRack = await applicationDBContext.ServerRacks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return new()
            {
                Success = true,
                Object = serverRack
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
    public async Task<Result<int>> GetNumberServerRacks()
    {
        try
        {
            using SudInfoDbContext sudInfoDbContext = new();
            int numberServerRacks = await sudInfoDbContext.ServerRacks.CountAsync();
            return new()
            {
                Object = numberServerRacks,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message,
                Success = false
            };
        }
    }
    public async Task<Result> RemoveServerRack(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var serverRack = await applicationDBContext.ServerRacks.SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("ServerRack not found");
            applicationDBContext.Remove(serverRack);
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
    public async Task<Result> UpdateServerRack(ServerRack entity)
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