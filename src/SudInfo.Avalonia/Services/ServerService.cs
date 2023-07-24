namespace SudInfo.Avalonia.Services;

public class ServerService : BaseService
{
    public static async Task<Result<Server>> GetServer(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Servers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return server == null
                ? throw new Exception("Server not Found")
                : new()
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

    public static async Task<Result> RemoveServer(int id)
    {
        try
        {
            await using SudInfoDbContext applicationDBContext = new();
            Server server = (await applicationDBContext.Servers.SingleOrDefaultAsync(x => x.Id == id)) ?? throw new Exception("Server not found");
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
                Message = ex.Message
            };
        }
    }

    public static async Task<Result> UpServerPositionInServerRack(int id)
    {
        await using SudInfoDbContext applicationDBContext = new();
        Server? server = await applicationDBContext.Servers
                                                   .Include(x => x.ServerRack)
                                                   .ThenInclude(x => x.Servers)
                                                   .SingleOrDefaultAsync(x => x.Id == id);
        if (server == null)
            return new(message: "Сервер не найден");
        if (server.PosiitionInServerRack == 1)
            return new(message: "Сервер уже находится в самом вверху");
        var previousServer = await applicationDBContext.Servers.FirstOrDefaultAsync(x => x.ServerRackId == server.ServerRackId &&
                                                                                                     x.PosiitionInServerRack == server.PosiitionInServerRack - 1);
        previousServer.PosiitionInServerRack++;
        server.PosiitionInServerRack--;
        await applicationDBContext.SaveChangesAsync();
        return new(true);
    }

    public static async Task<Result> DownServerPositionInServerRack(int id)
    {
        await using SudInfoDbContext applicationDBContext = new();
        Server? server = await applicationDBContext.Servers
                                                   .Include(x => x.ServerRack)
                                                   .ThenInclude(x => x.Servers)
                                                   .SingleOrDefaultAsync(x => x.Id == id);
        if (server == null)
            return new(message: "Сервер не найден");
        if (server.PosiitionInServerRack == server.ServerRack?.Servers.Count)
            return new(message: "Сервер уже находится в самом низу");
        var nextServer = await applicationDBContext.Servers.FirstOrDefaultAsync(x => x.ServerRackId == server.ServerRackId &&
                                                                                             x.PosiitionInServerRack == server.PosiitionInServerRack + 1);
        nextServer.PosiitionInServerRack--;
        server.PosiitionInServerRack++;
        await applicationDBContext.SaveChangesAsync();
        return new(true);
    }
}