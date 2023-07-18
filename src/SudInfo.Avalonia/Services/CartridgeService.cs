namespace SudInfo.Avalonia.Services;

public class CartridgeService : BaseService
{
    #region Get Methods

    public static async Task<Result<Cartridge>> GetCartridge(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var cartridge = await applicationDBContext.Cartridges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return cartridge == null
                ? throw new Exception("Cartridge not Found")
                : new()
                {
                    Success = true,
                    Object = cartridge
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
    public static async Task<IReadOnlyCollection<Cartridge>> GetCartridges()
    {
        using SudInfoDbContext applicationDBContext = new();
        var cartridges = await applicationDBContext.Cartridges.AsNoTracking().ToListAsync();
        return cartridges;
    }

    #endregion

    public static async Task<Result> Remove(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var cartridge = await applicationDBContext.Cartridges.FirstOrDefaultAsync(x => x.Id == id);
            applicationDBContext.Cartridges.Remove(cartridge);
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