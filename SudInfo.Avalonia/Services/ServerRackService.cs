namespace SudInfo.Avalonia.Services;

public class ServerRackService : BaseService
{
    public static async Task<Result<IReadOnlyList<ServerRack>>> GetServerRacksWithServers()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var serverRacks = await applicationDBContext.ServerRacks
                .AsNoTracking()
                .Include(x => x.Servers)
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
    public static async Task<Result<ServerRack>> GetServerRack(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var serverRack = await applicationDBContext.ServerRacks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Серверна стойка не найдена");
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
    public static async Task<int> GetNumberServerRacks()
    {
        using SudInfoDbContext sudInfoDbContext = new();
        int numberServerRacks = await sudInfoDbContext.ServerRacks.CountAsync();
        return numberServerRacks;
    }
    public static async Task<Result> RemoveServerRack(int id)
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
}