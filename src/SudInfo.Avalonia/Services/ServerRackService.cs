namespace SudInfo.Avalonia.Services;

public class ServerRackService : BaseService<ServerRack>
{
    #region Ctors

    public ServerRackService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<Result<IReadOnlyCollection<ServerRack>>> Get()
    {
        try
        {
            var serverRacks = await context.ServerRacks.AsNoTracking()
                                                       .Include(x => x.Servers)
                                                       .ToListAsync();
            foreach (var serverRack in serverRacks)
            {
                serverRack.Servers = serverRack.Servers.OrderBy(x => x.PosiitionInServerRack)
                                                       .ToList();
            }
            return new(serverRacks, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<Result<ServerRack>> Get(int id)
    {
        try
        {
            var serverRack = await context.ServerRacks.AsNoTracking()
                                                      .FirstAsync(x => x.Id == id);
            return new(serverRack, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<int> GetNumberServerRacks()
    {
        int numberServerRacks = await context.ServerRacks.CountAsync();
        return numberServerRacks;
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var serverRack = await context.ServerRacks.AsNoTracking()
                                                      .FirstAsync(x => x.Id == id);
            context.Remove(serverRack);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}