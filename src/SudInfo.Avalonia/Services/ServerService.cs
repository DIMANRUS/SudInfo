namespace SudInfo.Avalonia.Services;

public class ServerService : BaseService<Server>
{
    #region Ctors

    public ServerService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    public async Task<Result<Server>> Get(int id)
    {
        try
        {
            var server = await context.Servers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return server == null
                ? throw new Exception("Server not Found")
                : new(server, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<Result> Remove(int id)
    {
        try
        {
            Server server = (await context.Servers.SingleOrDefaultAsync(x => x.Id == id)) ?? throw new Exception("Server not found");
            context.Remove(server);
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

    public async Task<Result> UpServerPositionInServerRack(int id)
    {
        Server? server = await context.Servers
                                                   .Include(x => x.ServerRack)
                                                   .ThenInclude(x => x.Servers)
                                                   .SingleOrDefaultAsync(x => x.Id == id);
        if (server == null)
            return new(message: "Сервер не найден");
        if (server.PosiitionInServerRack == 1)
            return new(message: "Сервер уже находится в самом вверху");
        var previousServer = await context.Servers.FirstOrDefaultAsync(x => x.ServerRackId == server.ServerRackId &&
                                                                                                     x.PosiitionInServerRack == server.PosiitionInServerRack - 1);
        previousServer.PosiitionInServerRack++;
        server.PosiitionInServerRack--;
        await context.SaveChangesAsync();
        return new(true);
    }

    public async Task<Result> DownServerPositionInServerRack(int id)
    {
        Server? server = await context.Servers
                                                   .Include(x => x.ServerRack)
                                                   .ThenInclude(x => x.Servers)
                                                   .SingleOrDefaultAsync(x => x.Id == id);
        if (server == null)
            return new(message: "Сервер не найден");
        if (server.PosiitionInServerRack == server.ServerRack?.Servers.Count)
            return new(message: "Сервер уже находится в самом низу");
        var nextServer = await context.Servers.FirstOrDefaultAsync(x => x.ServerRackId == server.ServerRackId &&
                                                                                             x.PosiitionInServerRack == server.PosiitionInServerRack + 1);
        nextServer.PosiitionInServerRack--;
        server.PosiitionInServerRack++;
        await context.SaveChangesAsync();
        return new(true);
    }
}