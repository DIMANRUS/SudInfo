namespace SudInfo.Avalonia.Services;

public class PeripheryService : BaseService
{
    #region Get Methods

    public static async Task<Result<Periphery>> GetPeripheryById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var periphery = await applicationDBContext.Peripheries.Include(x => x.Computer).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (periphery == null)
                throw new Exception("Пепреферия не найдена");
            return new()
            {
                Success = true,
                Object = periphery
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
    public static async Task<IReadOnlyList<Periphery>> GetPeripheryList()
    {
        using SudInfoDbContext applicationDBContext = new();
        var peripheries = await applicationDBContext.Peripheries
            .Include(x => x.Computer)
            .ThenInclude(x => x.User)
            .ToListAsync();
        return peripheries;
    }

    #endregion

    public static async Task<Result> AddPeriphery(Periphery periphery)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            if (periphery.Computer != null)
            {
                periphery.Computer = await applicationDBContext.Computers.SingleOrDefaultAsync(x => x.Id == periphery.Computer.Id);
            }
            await applicationDBContext.Peripheries.AddAsync(periphery);
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
    public static async Task<Result> RemovePeripheryById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var periphery = await applicationDBContext.Peripheries.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Переферия не найдена");
            applicationDBContext.Peripheries.Remove(periphery);
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